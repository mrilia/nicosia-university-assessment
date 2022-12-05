using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Result>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;

        public UpdateAdminCommandHandler(IAdminContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var adminToUpdate = await GetAdminAsync(request, cancellationToken);

            if (adminToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.AdminNotFound)));

            _mapper.Map(request, adminToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Admin> GetAdminAsync(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            return await _context.Admins.SingleOrDefaultAsync(x => x.AdminId == request.AdminId, cancellationToken);
        }

    }
}
