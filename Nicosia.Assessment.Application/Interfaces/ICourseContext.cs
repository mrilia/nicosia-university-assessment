using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Nicosia.Assessment.Domain.Models.Course;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface ICourseContext
    {
        DbSet<Course> Courses { get; set; }
        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
