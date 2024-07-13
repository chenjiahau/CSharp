namespace TodoAPI.Models.DTOs
{
	public class WorkDTO
	{
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}