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

            services.AddScoped<IDbContext>(provider => provider.GetService<SqliteDbContext>());
            //services.AddScoped<IStudentContext>(provider => provider.GetService<StudentContext>());
            //services.AddScoped<IUserContext>(provider => provider.GetService<UserContext>());
            //services.AddScoped<ILecturerContext>(provider => provider.GetService<LecturerContext>());
            //services.AddScoped<ICourseContext>(provider => provider.GetService<CourseContext>());
            //services.AddScoped<IPeriodContext>(provider => provider.GetService<PeriodContext>());
            //services.AddScoped<ISectionContext>(provider => provider.GetService<SectionContext>());

            return services;
        }
    }
}
