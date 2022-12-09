using System.IO;
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
            var useSqlite = bool.Parse(configuration["UseSqliteAsDatabaseEngine"]!);
            var usePostgres = bool.Parse(configuration["UsePostgresAsDatabaseEngine"]!);

            if (!useSqlite && !usePostgres)
                throw new InvalidDataException("No database engine has been selected to start the project!\nPlease config the appsettings.json file correctly.");

            if (useSqlite && usePostgres)
                throw new InvalidDataException("Multiple database engine has been selected to start the project!\nPlease config the appsettings.json file correctly.");

            if (useSqlite)
            {
                services.AddDbContext<SqliteDbContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("SqliteDbContext"));

                    options.EnableSensitiveDataLogging();
                });

                services.AddScoped<IAdminContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<IStudentContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<ILecturerContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<ICourseContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<IPeriodContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<ISectionContext>(provider => provider.GetService<SqliteDbContext>());
                services.AddScoped<IApprovalRequestContext>(provider => provider.GetService<SqliteDbContext>());
            }
            else if (usePostgres)
            {
                services.AddDbContext<PostgresDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("PostgresDbContext"));

                    options.EnableSensitiveDataLogging();
                });

                services.AddScoped<IAdminContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<IStudentContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<ILecturerContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<ICourseContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<IPeriodContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<ISectionContext>(provider => provider.GetService<PostgresDbContext>());
                services.AddScoped<IApprovalRequestContext>(provider => provider.GetService<PostgresDbContext>());
            }



            return services;
        }
    }
}
