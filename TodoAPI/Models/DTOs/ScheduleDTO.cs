namespace TodoAPI.Models.DTOs
{
	public class ScheduleDTO
	{
        public Guid Id { get; set; }
        public DateTime ExectionDate { get; set; }
        public UserDTO? User { get; set; }
        public WorkDTO? Work { get; set; }
    }
}