using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Section.Queries
{
    public class GetClassListQuery : PaginationRequest, IRequest<CollectionItems<SectionDto>>
    {
        public string Number { get; set; }
        public SectionSort Sort { get; set; }

    }


    public class GetClassListQueryHandler : IRequestHandler<GetClassListQuery, CollectionItems<SectionDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public GetClassListQueryHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<SectionDto>> Handle(GetClassListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Section.Section> sections = _context.Sections;

            if (!string.IsNullOrWhiteSpace(request.Number))
            {
                sections = sections.Where(x => x.Number.ToLower().Contains(request.Number.ToLower()));
            }

            sections = ApplySort(sections, request.Sort);
            var totalCount = await sections.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<SectionDto>>(await sections.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<SectionDto>(result, totalCount);
        }


        private static IQueryable<Domain.Models.Section.Section> ApplySort(IQueryable<Domain.Models.Section.Section> query, SectionSort sort = SectionSort.None)
        {
            query = sort switch
            {
                SectionSort.ByNumberAsc => query.OrderBy(r => r.Number),
                _ => query
            };

            return query;
        }
    }
}
