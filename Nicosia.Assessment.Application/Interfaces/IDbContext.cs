using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<Student> Students { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
