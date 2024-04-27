using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[Authorize]
[ApiController]
[Route("authors")]
public class AuthorController(AuthorService authorService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AuthorGet>> CreateAuthor(AuthorCreate authorCreate)
    {
        try
        {
            var author = mapper.Map<Author>(authorCreate);
            var createdAuthor = await authorService.CreateAsync(author);
            var authorGet = mapper.Map<AuthorGet>(createdAuthor);
            return CreatedAtAction(nameof(GetAuthor), new { id = authorGet.Id }, authorGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create author: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorGet>> GetAuthor(int id)
    {
        try
        {
            var author = await authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            var authorGet = mapper.Map<AuthorGet>(author);
            return Ok(authorGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get author: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorGet>>> GetAllAuthors()
    {
        try
        {
            var authors = await authorService.GetAllAsync();
            var authorsGet = mapper.Map<IEnumerable<AuthorGet>>(authors);
            return Ok(authorsGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all authors: {ex.Message}");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdate authorUpdate)
    {
        try
        {
            var author = mapper.Map<Author>(authorUpdate);
            var updatedAuthor = await authorService.UpdateAsync(author);
            if (updatedAuthor == null)
            {
                return NotFound("Author not found.");
            }
            return Ok(updatedAuthor);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update author: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            await authorService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete author: {ex.Message}");
        }
    }
}

