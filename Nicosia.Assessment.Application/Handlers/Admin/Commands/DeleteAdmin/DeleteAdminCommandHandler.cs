using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Result>
    {
        private readonly IAdminContext _context;

        public DeleteAdminCommandHandler(IAdminContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await GetAdminAsync(request, cancellationToken);

            if (admin is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.AdminNotFound)));

            _context.Admins.Remove(admin);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Admin> GetAdminAsync(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            return await _context.Admins.SingleOrDefaultAsync(x => x.AdminId == request.AdminId, cancellationToken);
        }

    }
}
