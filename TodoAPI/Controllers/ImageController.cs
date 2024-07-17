using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

		public ImageController(IImageRepository _imageRepository)
		{
            imageRepository = _imageRepository;

        }

        private void ValidateFile(UploadImageDTO uploadImageDTO)
        {
            var allowedExtensions = new string[] { ".jpg" };

            if (!allowedExtensions.Contains(Path.GetExtension(uploadImageDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extenstion");
            }

            if (uploadImageDTO.File.Length > 10485670)
            {
                ModelState.AddModelError("file", "File size more than 10MB");
            }
        }

        /**
         * URL: /api/Image/Upload
         * METHOD: POST
         */
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageDTO uploadImageDTO)
        {
            ValidateFile(uploadImageDTO);

            if (ModelState.IsValid)
            {
                var imageModel = new Image
                {
                    File = uploadImageDTO.File,
                    Name = uploadImageDTO.Name,
                    Extension = Path.GetExtension(uploadImageDTO.File.FileName),
                    Description = uploadImageDTO.Description,
                    SizeInBytes = uploadImageDTO.File.Length
                };

                var image = await imageRepository.Upload(imageModel);

                return Ok(image);
            }

            return BadRequest(ModelState);
        }
    }
}