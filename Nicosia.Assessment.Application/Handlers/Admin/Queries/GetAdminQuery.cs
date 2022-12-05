using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Queries
{
    public class GetAdminQuery : IRequest<Result<AdminDto>>
    {
        public Guid AdminId { get; set; }
    }

    public class GetAdminQueryHandler : IRequestHandler<GetAdminQuery, Result<AdminDto>>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;

        public GetAdminQueryHandler(IAdminContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<AdminDto>> Handle(GetAdminQuery request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.SingleOrDefaultAsync(x => x.AdminId == request.AdminId,
                cancellationToken);

            if (admin is null)
                return Result<AdminDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.AdminNotFound)));

            return Result<AdminDto>.SuccessFul(_mapper.Map<AdminDto>(admin));
        }
    }
}
