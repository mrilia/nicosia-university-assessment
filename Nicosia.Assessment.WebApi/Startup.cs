using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nicosia.Assessment.WebApi.Installer;
using Nicosia.Assessment.Application;
using Nicosia.Assessment.Persistence;
using Nicosia.Assessment.Persistence.Context;
using Nicosia.Assessment.WebApi.Middleware;

namespace Nicosia.Assessment.WebApi
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.InstallServicesAssembly(Configuration);
            services.AddApplication();
            services.AddPersistence(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nicosia.Assessment.WebApi v1"));
            // }

            var useSqlite = bool.Parse(Configuration["UseSqliteAsDatabaseEngine"]!);
            var usePostgres = bool.Parse(Configuration["UsePostgresAsDatabaseEngine"]!);

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (useSqlite)
                {
                    var dbContext = serviceScope?.ServiceProvider.GetRequiredService<SqliteDbContext>();
                    dbContext?.Database.EnsureCreated();
                    dbContext?.SeedDefaultData().Wait();
                }
                else if (usePostgres)
                {
                    var dbContext = serviceScope?.ServiceProvider.GetRequiredService<PostgresDbContext>();
                    dbContext?.Database.EnsureCreated();
                    dbContext?.SeedDefaultData().Wait();
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyPolicy");

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
