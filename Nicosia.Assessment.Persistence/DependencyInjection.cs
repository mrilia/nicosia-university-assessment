using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Persistence.Context;

namespace Nicosia.Assessment.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DbContext"));

                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IStudentContext>(provider => provider.GetService<SqliteDbContext>());
            services.AddScoped<ILecturerContext>(provider => provider.GetService<SqliteDbContext>());
            services.AddScoped<ICourseContext>(provider => provider.GetService<SqliteDbContext>());
            services.AddScoped<IPeriodContext>(provider => provider.GetService<SqliteDbContext>());
            services.AddScoped<ISectionContext>(provider => provider.GetService<SqliteDbContext>());

            return services;
        }
    }
}
