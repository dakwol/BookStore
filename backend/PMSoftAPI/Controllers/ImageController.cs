using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PMSoftAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("images")]
    public class ImageController(ImageService imageService) : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile? file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is empty");
                }

                var mediaPath = await imageService.Upload(file);
                return Ok(mediaPath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}