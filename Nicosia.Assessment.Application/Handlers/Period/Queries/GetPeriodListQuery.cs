using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Period.Queries
{
    public class GetPeriodListQuery : IRequest<List<PeriodDto>>
    {
        public string Name { get; set; }
    }


    public class GetPeriodListQueryHandler : IRequestHandler<GetPeriodListQuery, List<PeriodDto>>
    {
        private readonly IPeriodContext _context;
        private readonly IMapper _mapper;

        public GetPeriodListQueryHandler(IPeriodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PeriodDto>> Handle(GetPeriodListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Period.Period> periods = _context.Periods;

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                periods = periods.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            return _mapper.Map<List<PeriodDto>>(await periods.ToListAsync(cancellationToken));
        }
    }
}
