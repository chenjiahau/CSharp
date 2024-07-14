using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public class SQLWorkRepository : IWorkRepository
	{
        private readonly TodoDbContext dbContext;

        public SQLWorkRepository(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Work>> GetAll()
        {
            return await dbContext.Works.ToListAsync();
        }

        public async Task<Work?> GetById(Guid id)
        {
            return await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Work?> Add(Work work)
        {
            await dbContext.Works.AddAsync(work);
            await dbContext.SaveChangesAsync();

            return work;
        }

        public async Task<Work?> PutById(Guid id, Work work)
        {
            var existingWork = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWork == null)
            {
                return null;
            }

            existingWork.Title = work.Title;
            existingWork.Description = work.Description;

            await dbContext.SaveChangesAsync();

            return existingWork;
        }

        public async Task<Work?> DeleteById(Guid id)
        {
            var existingWork = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWork == null)
            {
                return null;
            }

            dbContext.Works.Remove(existingWork);
            await dbContext.SaveChangesAsync();

            return existingWork;
        }
    }
}