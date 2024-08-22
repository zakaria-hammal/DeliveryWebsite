using Microsoft.AspNetCore.Mvc;
using Projet.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class ImageController : ControllerBase
    {
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var name = imageName + ".png";

            var imagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),"Images"), name);

            if (!System.IO.File.Exists(imagePath))
            {
                imagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),"Images"), "default.png");
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);

            return File(imageBytes, "image/png");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ImageModel model)
        {
            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine("Images", $"{model.Description}.png");

#pragma warning disable CS8604 // Possible null reference argument.
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
#pragma warning restore CS8604 // Possible null reference argument.

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            return Ok(new { FilePath = filePath });
        }
    }
}