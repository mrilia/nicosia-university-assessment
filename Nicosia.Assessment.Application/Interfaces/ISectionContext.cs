using System.Threading;
using System.Threading.Tasks;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface ISectionContext
    {

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
