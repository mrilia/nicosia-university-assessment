using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface ILecturerContext
    {
        DbSet<Lecturer> Lecturers { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
