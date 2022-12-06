using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Period.Queries
{
    public class GetPeriodListQuery : PaginationRequest,IRequest<List<PeriodDto>>
    {
        public string Name { get; set; }
        public PeriodSort Sort { get; set; }

    }
    public enum PeriodSort
    {
        None,
        ByNameAsc,
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

            periods = ApplySort(periods, request.Sort);

            return _mapper.Map<List<PeriodDto>>(await periods.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));
        }

        private static IQueryable<Domain.Models.Period.Period> ApplySort(IQueryable<Domain.Models.Period.Period> query, PeriodSort sort)
        {
            query = sort switch
            {
                PeriodSort.ByNameAsc => query.OrderBy(r => r.Name),
                _ => query
            };

            return query;
        }
    }
}
