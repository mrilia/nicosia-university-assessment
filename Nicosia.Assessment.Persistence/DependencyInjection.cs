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
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CustomerContext"));

                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<ICustomerContext>(provider => provider.GetService<CustomerContext>());

            return services;
        }
    }
}
