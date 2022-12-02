
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Domain.Models.Customer;

namespace Nicosia.Assessment.Persistence.Context
{
    public class CustomerContext : DbContext, ICustomerContext
    {
        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=Database.db");
            }
        }

        public DbSet<Customer> Customers { get; set; }


        public Task SaveAsync(CancellationToken cancellationToken) =>
            base.SaveChangesAsync(cancellationToken);

        public Task CloseConnection() =>
            base.Database.CloseConnectionAsync();


        public Task CloseConnection(CancellationToken cancellationToken)
            => base.Database.CloseConnectionAsync();

        public void Save() =>
            base.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }
}
