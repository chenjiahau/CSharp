using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public interface IScheduleRepository
	{
        Task<List<Schedule>> GetAll(string? column, string? keyword);
        Task<Schedule?> GetById(Guid id);
        Task<Schedule?> Add(Schedule schedule);
        Task<Schedule?> PutById(Guid id, Schedule schedule);
        Task<Schedule?> DeleteById(Guid id);
    }
}