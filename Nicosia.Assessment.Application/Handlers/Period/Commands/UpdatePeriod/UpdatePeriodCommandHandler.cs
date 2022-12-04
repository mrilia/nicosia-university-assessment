using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.UpdatePeriod
{
    public class UpdatePeriodCommandHandler : IRequestHandler<UpdatePeriodCommand, Result>
    {
        private readonly IPeriodContext _context;
        private readonly IMapper _mapper;

        public UpdatePeriodCommandHandler(IPeriodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            var periodToUpdate = await GetPeriodAsync(request, cancellationToken);

            if (periodToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.PeriodNotFound)));

            _mapper.Map(request, periodToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Period.Period> GetPeriodAsync(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            return await _context.Periods.SingleOrDefaultAsync(x => x.PeriodId == request.PeriodId, cancellationToken);
        }

    }
}
