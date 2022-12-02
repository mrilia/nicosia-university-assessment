using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.Customer;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface ICustomerContext
    {
        DbSet<Customer> Customers { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
