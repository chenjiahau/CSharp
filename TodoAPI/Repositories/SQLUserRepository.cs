using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public class SQLUserRepository : IUserRepository
	{
        private readonly TodoDbContext dbContext;

        public SQLUserRepository(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<User>> GetAll(string? column, string? keyword)
        {
            var users = dbContext.Users.AsQueryable();

            if (
                string.IsNullOrWhiteSpace(column) == false &&
                string.IsNullOrWhiteSpace(keyword) == false
            ) {
                if (column.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Name.Contains(keyword));
                }


                if (column.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Email.Contains(keyword));
                }
            }

            return await users.ToListAsync();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> Add(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> PutById(Guid id, User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            await dbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User?> DeleteById(Guid id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}