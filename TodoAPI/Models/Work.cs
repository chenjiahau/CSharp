﻿namespace TodoAPI.Models
{
    public class Work
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}