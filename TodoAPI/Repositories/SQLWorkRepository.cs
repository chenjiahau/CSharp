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

        public async Task<List<Work>> GetAll(
            string? column,
            string? keyword,
            string? sortBy,
            bool isAsc = true,
            int pageNumber = 1,
            int pageSize = 1
        ) {
            var works = dbContext.Works.AsQueryable();

            if (
                string.IsNullOrWhiteSpace(column) == false &&
                string.IsNullOrWhiteSpace(keyword) == false
            )
            {
                if (column.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    works = works.Where(x => x.Title.Contains(keyword));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    works = isAsc ? works.OrderBy(x => x.Title) : works.OrderByDescending(x => x.Title);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await works.Skip(skipResults).Take(pageSize).ToListAsync();
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