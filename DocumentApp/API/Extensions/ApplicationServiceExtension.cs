using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, 
            IConfiguration config
        ) {
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Application.Categories.List.Handler).Assembly)
            );

            services.AddAutoMapper(typeof(Application.Categories.List.Handler).Assembly);

            return services;
        }
    }
}