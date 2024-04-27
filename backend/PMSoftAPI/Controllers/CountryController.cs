using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[Authorize]
[ApiController]
[Route("countries")]
public class CountryController(CountryService countryService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CountryGet>> CreateCountry(CountryCreate countryCreate)
    {
        try
        {
            var country = mapper.Map<Country>(countryCreate);
            var createdCountry = await countryService.CreateAsync(country);
            var countryGet = mapper.Map<CountryGet>(createdCountry);
            return CreatedAtAction(nameof(GetCountry), new { id = countryGet.Id }, countryGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create country: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryGet>> GetCountry(int id)
    {
        try
        {
            var country = await countryService.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound("Country not found.");
            }
            var countryGet = mapper.Map<CountryGet>(country);
            return Ok(countryGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get country: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryGet>>> GetAllCountries()
    {
        try
        {
            var countries = await countryService.GetAllAsync();
            var countriesGet = mapper.Map<IEnumerable<CountryGet>>(countries);
            return Ok(countriesGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all countries: {ex.Message}");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCountry(int id, CountryUpdate countryUpdate)
    {
        try
        {
            var country = mapper.Map<Country>(countryUpdate);
            var updatedCountry = await countryService.UpdateAsync(country);
            if (updatedCountry == null)
            {
                return NotFound("Country not found.");
            }
            return Ok(updatedCountry);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update country: {ex.Message}");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            await countryService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete country: {ex.Message}");
        }
    }
}

