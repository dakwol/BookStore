using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PMSoftAPI.Models;
using PMSoftAPI.Services;

namespace PMSoftAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController(UserService userService, TokenService tokenService, IMapper mapper) : ControllerBase
{
    private readonly UserService _userService = userService;
    private readonly TokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<UserGet>> CreateUser(UserCreate user)
    {
        var userDomain = _mapper.Map<User>(user);
        
        try
        {
            var userCreated = await _userService.CreateAsync(userDomain);
            
            var userGet = _mapper.Map<UserGet>(userCreated);
            
            return CreatedAtAction(nameof(GetUser), new { id = userGet.Id }, userGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create user: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserGet>> GetUser(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            
            if (user == null)
            {
                return NotFound("User not found.");
            }
            
            var userGet = _mapper.Map<UserGet>(user);
            return userGet;
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get user: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserGet>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllAsync();

            var usersGet = _mapper.Map<IEnumerable<UserGet>>(users);
            return Ok(usersGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all users: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete user: {ex.Message}");
        }
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<TokenApiModel?>> RegisterUser(UserCreate user)
    {
        try
        {
            var existingUser = await _userService.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return BadRequest("User with the same email already exists.");
            }
                
            var userDto = _mapper.Map<User>(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            userDto.RefreshToken = refreshToken;
            userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(31);
            
            userDto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            var userNew = await _userService.CreateAsync(userDto);
                
            var claims = new List<Claim>
            {
                new Claim("Name", userNew.Name ?? ""),
                new Claim("Email", userNew.Email),
                new Claim("Id", userNew.Id.ToString()),
            };
                
            var accessToken = _tokenService.GenerateAccessToken(claims);
                    
            var tokens = new TokenApiModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

                
            return Ok(tokens);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<TokenApiModel?>> LoginUser(UserLogin user)
    {
        try
        {
            var userDto = await _userService.GetByEmailAsync(user.Email);
            
            if (userDto == null)
            {
                return NotFound("User not found");
            }
            
            var isAuthenticated = BCrypt.Net.BCrypt.Verify(user.Password, userDto.PasswordHash);

            if (isAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim("Name", userDto.Name ?? ""),
                    new Claim("Email", userDto.Email),
                    new Claim("Id", userDto.Id.ToString()),
                };
                
                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                    
                userDto.RefreshToken = refreshToken;
                userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(31);

                await _userService.UpdateAsync(userDto);
                
                var tokens = new TokenApiModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                
                return Ok(tokens);
            }
            else
            {
                return Unauthorized("Invalid password");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
