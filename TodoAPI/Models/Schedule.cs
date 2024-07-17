﻿namespace TodoAPI.Models
{
	public class Schedule
	{
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public DateTime ExectionDate { get; set; }

        public bool IsActived { get; set; }

        public Guid UserId { get; set; }

        public Guid WorkId { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Work? Work { get; set; }
    }
}