using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;
using PMSoftAPI.Services;

namespace PMSoftAPI.Controllers;

[ApiController]
[Route("tokens")]
public class TokenController(UserService userService, TokenService tokenService) : ControllerBase
{
    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(TokenApiModel? tokenApiModel)
    {
        if (tokenApiModel is null)
            return BadRequest("Invalid client request");

        var accessToken = tokenApiModel.AccessToken;
        var refreshToken = tokenApiModel.RefreshToken;

        var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
        var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == "Email");

        if (emailClaim == null)
            return BadRequest("Invalid client request");

        var email = emailClaim.Value;

        var user = await userService.GetByEmailAsync(email);
        
        if (user == null || user.RefreshToken != refreshToken)
            return BadRequest("Invalid client request");
        
        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest("Token expired");

        var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime =  DateTime.UtcNow.AddDays(31);
        await userService.UpdateAsync(user);

        return Ok(new
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Revoke(TokenApiModel? tokenApiModel)
    {
        if (tokenApiModel is null)
            return BadRequest("Invalid client request");

        var accessToken = tokenApiModel.AccessToken;
        var refreshToken = tokenApiModel.RefreshToken;

        var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
        var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == "Email");

        if (emailClaim == null)
            return BadRequest("Invalid client request");

        var email = emailClaim.Value;

        var user = await userService.GetByEmailAsync(email);
        if (user == null) return BadRequest();

        user.RefreshToken = null;
        await userService.UpdateAsync(user);

        return NoContent();
    }
}