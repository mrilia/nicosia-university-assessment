using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Queries
{
    public class GetPeriodQuery : IRequest<Result<PeriodDto>>
    {
        public Guid PeriodId { get; set; }
    }

    public class GetPeriodQueryHandler : IRequestHandler<GetPeriodQuery, Result<PeriodDto>>
    {
        private readonly IPeriodContext _context;
        private readonly IMapper _mapper;

        public GetPeriodQueryHandler(IPeriodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PeriodDto>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
        {
            var Period = await _context.Periods.SingleOrDefaultAsync(x => x.PeriodId == request.PeriodId,
                cancellationToken);

            if (Period is null)
                return Result<PeriodDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.PeriodNotFound)));

            return Result<PeriodDto>.SuccessFul(_mapper.Map<PeriodDto>(Period));
        }
    }
}
