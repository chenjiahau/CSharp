using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public class SQLScheduleRepository : IScheduleRepository
    {
        private readonly TodoDbContext dbContext;

        public SQLScheduleRepository(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Schedule>> GetAll()
        {
            return await dbContext.Schedules.Include("User").Include("Work").ToListAsync();
        }

        public async Task<Schedule?> GetById(Guid id)
        {
            return await dbContext.Schedules.Include("User").Include("Work").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Schedule?> Add(Schedule schedule)
        {
            await dbContext.Schedules.AddAsync(schedule);
            await dbContext.SaveChangesAsync();

            var newSchedule = await GetById(schedule.Id);

            return newSchedule;
        }

        public async Task<Schedule?> PutById(Guid id, Schedule schedule)
        {
            var existingSchedule = await GetById(id);

            if (existingSchedule == null)
            {
                return null;
            }

            existingSchedule.UserId = schedule.UserId;
            existingSchedule.WorkId = schedule.WorkId;

            await dbContext.SaveChangesAsync();

            return existingSchedule;
        }

        public async Task<Schedule?> DeleteById(Guid id)
        {
            var existingSchedule = await GetById(id);

            if (existingSchedule == null)
            {
                return null;
            }

            dbContext.Schedules.Remove(existingSchedule);
            await dbContext.SaveChangesAsync();

            return existingSchedule;
        }
    }
}