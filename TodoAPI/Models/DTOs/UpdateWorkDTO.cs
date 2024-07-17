using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
	public class UpdateWorkDTO
	{
        [Required]
        [MinLength(8, ErrorMessage = "Title's length is 8 to 32")]
        [MaxLength(32, ErrorMessage = "Title's length is 8 to 32")]
        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}