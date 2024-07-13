namespace TodoAPI.Models.DTOs
{
	public class AddScheduleDTO
	{
        public DateTime ExectionDate { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkId { get; set; }
    }
}