using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext dataContext)
        {
            if (dataContext.Categories.Any()) return;

            var categories = new List<Category>
            {
                new Category
                {
                    Title = "Reading",
                    Description = "Books, articles, and other reading materials",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Writing",
                    Description = "Writing materials and tools",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Coding",
                    Description = "Programming languages, tools, and resources",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Drawing",
                    Description = "Drawing materials and tools",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Music",
                    Description = "Musical instruments and resources",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Cooking",
                    Description = "Cooking tools and ingredients",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Exercise",
                    Description = "Exercise equipment and resources",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Gaming",
                    Description = "Video games and gaming consoles",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Photography",
                    Description = "Photography equipment and resources",
                    IsDeleted = false
                },
                new Category
                {
                    Title = "Travel",
                    Description = "Travel destinations and resources",
                    IsDeleted = false
                }
            };

            await dataContext.Categories.AddRangeAsync(categories);
            await dataContext.SaveChangesAsync();
        }
    }
}