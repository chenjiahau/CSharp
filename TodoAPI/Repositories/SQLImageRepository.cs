using System;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class SQLImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TodoDbContext dbContext;

        public SQLImageRepository(
            IWebHostEnvironment _webHostEnvironment,
            IHttpContextAccessor _httpContextAccessor,
            TodoDbContext _dbContext
        )
        {
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            dbContext = _dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var name = $"{string.Format(@"{0}", Guid.NewGuid())}{image.Extension}";
            var path = Path.Combine(
                webHostEnvironment.ContentRootPath,
                "wwwroot/images",
                name
            );

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.Path}/upload/{image.Name}{image.Extension}";
            image.Name = name;
            image.Path = urlFilePath;

            using var stream = new FileStream(path, FileMode.Create);
            await image.File.CopyToAsync(stream);

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}

