namespace TodoAPI.Models.DTOs
{
	public class UpdateScheduleDTO
	{
        public string? Title { get; set; }
        public DateTime ExectionDate { get; set; }
        public bool IsActived { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkId { get; set; }
    }
}