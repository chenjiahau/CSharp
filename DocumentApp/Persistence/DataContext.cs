using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Category> Categories { get; set; }
    }
}