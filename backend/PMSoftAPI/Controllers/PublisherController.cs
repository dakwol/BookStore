using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[Authorize]
[ApiController]
[Route("publishers")]
public class PublisherController(PublisherService publisherService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PublisherGet>> CreatePublisher(PublisherCreate publisherCreate)
    {
        try
        {
            var publisher = mapper.Map<Publisher>(publisherCreate);
            var createdPublisher = await publisherService.CreateAsync(publisher);
            var publisherGet = mapper.Map<PublisherGet>(createdPublisher);
            return CreatedAtAction(nameof(GetPublisher), new { id = publisherGet.Id }, publisherGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create publisher: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PublisherGet>> GetPublisher(int id)
    {
        try
        {
            var publisher = await publisherService.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }
            var publisherGet = mapper.Map<PublisherGet>(publisher);
            return publisherGet;
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get publisher: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublisherGet>>> GetAllPublishers()
    {
        try
        {
            var publishers = await publisherService.GetAllAsync();
            var publishersGet = mapper.Map<IEnumerable<PublisherGet>>(publishers);
            return Ok(publishersGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all publishers: {ex.Message}");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePublisher(int id, PublisherUpdate publisherUpdate)
    {
        try
        {
            var publisher = mapper.Map<Publisher>(publisherUpdate);
            var updatedPublisher = await publisherService.UpdateAsync(publisher);
            if (updatedPublisher == null)
            {
                return NotFound("Publisher not found.");
            }
            return Ok(updatedPublisher);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update publisher: {ex.Message}");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        try
        {
            await publisherService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete publisher: {ex.Message}");
        }
    }
}

