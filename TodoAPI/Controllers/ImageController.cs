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

        private static readonly Dictionary<string, List<byte[]>> fileSignatures = new()
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            { ".jpeg2000", new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A } } }, 
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
         };

        private void ValidateFile(UploadImageDTO uploadImageDTO)
        {
            var allowedExtensions = new string[] { ".gif", ".png", ".jpg", ".jpeg" };

            if (!allowedExtensions.Contains(Path.GetExtension(uploadImageDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extenstion");
            }

            using (var reader = new BinaryReader(uploadImageDTO.File.OpenReadStream()))
            {
                var signatures = fileSignatures.Values.SelectMany(x => x).ToList();
                var headerBytes = reader.ReadBytes(fileSignatures.Max(m => m.Value.Max(n => n.Length)));
                bool result = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));

                if (!result)
                {
                    ModelState.AddModelError("file", "File is invalid");
                }
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