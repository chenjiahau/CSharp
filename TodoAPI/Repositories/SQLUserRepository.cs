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

        public async Task<List<User>> GetAll(
            string? column,
            string? keyword,
            string? sortBy,
            bool isAsc = true,
            int pageNumber = 1,
            int pageSize = 1
        ) {
            var users = dbContext.Users.AsQueryable();

            if (
                string.IsNullOrWhiteSpace(column) == false &&
                string.IsNullOrWhiteSpace(keyword) == false
            )
            {
                if (column.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Name.Contains(keyword));
                }


                if (column.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Email.Contains(keyword));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAsc ? users.OrderBy(x => x.Name) : users.OrderByDescending(x => x.Name);
                }


                if (sortBy.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAsc ? users.OrderBy(x => x.Email) : users.OrderByDescending(x => x.Email);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await users.Skip(skipResults).Take(pageSize).ToListAsync();
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