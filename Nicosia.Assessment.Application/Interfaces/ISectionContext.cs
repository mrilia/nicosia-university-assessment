using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Nicosia.Assessment.Domain.Models.Section;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface ISectionContext
    {
        DbSet<Section> Sections { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
