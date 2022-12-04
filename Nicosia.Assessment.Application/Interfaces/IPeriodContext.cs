using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.Period;
using System.Threading;
using System.Threading.Tasks;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface IPeriodContext
    {
        DbSet<Period> Periods { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
