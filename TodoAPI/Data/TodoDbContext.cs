using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}