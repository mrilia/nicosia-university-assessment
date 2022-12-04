using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.AddNewPeriod
{
    public class AddNewPeriodCommandHandler : IRequestHandler<AddNewPeriodCommand, Result<PeriodDto>>
    {
        private readonly IPeriodContext _context;
        private readonly IMapper _mapper;

        public AddNewPeriodCommandHandler(IPeriodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PeriodDto>> Handle(AddNewPeriodCommand request, CancellationToken cancellationToken)
        {
            var periodToAdd = _mapper.Map<Domain.Models.Period.Period>(request);

            await _context.Periods.AddAsync(periodToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<PeriodDto>.SuccessFul(_mapper.Map<PeriodDto>(periodToAdd));
        }
    }
}
