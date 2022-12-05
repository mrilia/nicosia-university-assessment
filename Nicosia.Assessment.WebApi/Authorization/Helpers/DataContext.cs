using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nicosia.Assessment.WebApi.Authorization.Entities;

namespace Nicosia.Assessment.WebApi.Authorization.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=Database.db");
        }
    }
}
