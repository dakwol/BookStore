using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[Authorize]
[ApiController]
[Route("genres")]
public class GenreController(GenreService genreService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GenreGet>> CreateGenre(GenreCreate genreCreate)
    {
        try
        {
            var genre = mapper.Map<Genre>(genreCreate);
            var createdGenre = await genreService.CreateAsync(genre);
            var genreGet = mapper.Map<GenreGet>(createdGenre);
            return CreatedAtAction(nameof(GetGenre), new { id = genreGet.Id }, genreGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create genre: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreGet>> GetGenre(int id)
    {
        try
        {
            var genre = await genreService.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }
            var genreGet = mapper.Map<GenreGet>(genre);
            return genreGet;
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get genre: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreGet>>> GetAllGenres()
    {
        try
        {
            var genres = await genreService.GetAllAsync();
            var genresGet = mapper.Map<IEnumerable<GenreGet>>(genres);
            return Ok(genresGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all genres: {ex.Message}");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreUpdate genreUpdate)
    {
        try
        {
            var genre = mapper.Map<Genre>(genreUpdate);
            var updatedGenre = await genreService.UpdateAsync(genre);
            if (updatedGenre == null)
            {
                return NotFound("Genre not found.");
            }
            return Ok(updatedGenre);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update genre: {ex.Message}");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            await genreService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete genre: {ex.Message}");
        }
    }
}

