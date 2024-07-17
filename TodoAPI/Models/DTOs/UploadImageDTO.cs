using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
	public class UploadImageDTO
	{
        [Required]
        public IFormFile? File { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}