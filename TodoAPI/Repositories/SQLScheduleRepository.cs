﻿using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Schedule>> GetAll(string? column, string? keyword)
        {
            var schedules = dbContext.Schedules.Include("User").Include("Work").AsQueryable();

            if (
                string.IsNullOrWhiteSpace(column) == false &&
                string.IsNullOrWhiteSpace(keyword) == false
            )
            {
                if (column.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    schedules = schedules.Where(x => x.Title.Contains(keyword));
                }
            }

            return await schedules.ToListAsync();
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

            existingSchedule.Title = schedule.Title;
            existingSchedule.ExectionDate = schedule.ExectionDate;
            existingSchedule.IsActived = schedule.IsActived;
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