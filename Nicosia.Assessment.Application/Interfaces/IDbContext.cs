using System.Threading;
using System.Threading.Tasks;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface IBaseContext
    {
        Task SaveAsync(CancellationToken cancellationToken);
        Task SeedDefaultData();
        Task CloseConnection();
    }
}
