using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.DeletePeriod
{
    public class DeletePeriodCommandHandler : IRequestHandler<DeletePeriodCommand, Result>
    {
        private readonly IPeriodContext _context;

        public DeletePeriodCommandHandler(IPeriodContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeletePeriodCommand request, CancellationToken cancellationToken)
        {
            var Period = await GetPeriodAsync(request, cancellationToken);

            if (Period is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.PeriodNotFound)));

            _context.Periods.Remove(Period);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Period.Period> GetPeriodAsync(DeletePeriodCommand request, CancellationToken cancellationToken)
        {
            return await _context.Periods.SingleOrDefaultAsync(x => x.PeriodId == request.PeriodId, cancellationToken);
        }

    }
}
