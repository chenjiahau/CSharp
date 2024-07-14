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

        public async Task<List<User>> GetAll()
        {
            return await dbContext.Users.ToListAsync();
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