using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public interface IImageRepository
	{
        Task<Image> Upload(Image image);
    }
}