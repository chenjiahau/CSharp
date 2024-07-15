using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
	public class AddScheduleDTO
	{
        [Required]
        [MinLength(8, ErrorMessage = "Title's length is 8 to 32")]
        [MaxLength(32, ErrorMessage = "Title's length is 8 to 32")]
        public string? Title { get; set; }
        public DateTime ExectionDate { get; set; }
        public bool IsActived { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkId { get; set; }
    }
}