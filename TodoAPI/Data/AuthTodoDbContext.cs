
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Data
{
	public class AuthTodoDbContext : IdentityDbContext
	{
		public AuthTodoDbContext(DbContextOptions<AuthTodoDbContext> options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "2efbc45d-53e6-4d3e-a21e-6894ad6fabc9";
            var writerId = "558a9dca-0fdd-493b-9d38-ba629502a1f4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

