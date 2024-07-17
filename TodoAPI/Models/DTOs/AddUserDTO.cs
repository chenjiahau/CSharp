using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
	public class AddUserDTO
	{
        [Required]
        [MinLength(8, ErrorMessage = "Name's length is 8 to 32")]
        [MaxLength(32, ErrorMessage = "Name's length is 8 to 32")]
        public string? Name { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Email's length is 8 to 32")]
        [MaxLength(128, ErrorMessage = "Email's length is 8 to 128")]
        public string? Email { get; set; }
    }
}