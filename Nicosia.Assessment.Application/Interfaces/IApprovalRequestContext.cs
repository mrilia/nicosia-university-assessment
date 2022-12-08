using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Interfaces
{
    public interface IApprovalRequestContext
    {
        DbSet<ApprovalRequest> ApprovalRequests { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
