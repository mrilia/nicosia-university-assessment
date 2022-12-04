using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Section.Queries
{
    public class GetSectionListQuery : IRequest<List<SectionDto>>
    {
        public string Number { get; set; }
    }


    public class GetSectionListQueryHandler : IRequestHandler<GetSectionListQuery, List<SectionDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public GetSectionListQueryHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SectionDto>> Handle(GetSectionListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Section.Section> sections = _context.Sections
                                                                            .Include(i=>i.Course)
                                                                            .Include(i=>i.Period);

            if (!string.IsNullOrWhiteSpace(request.Number))
            {
                sections = sections.Where(x => x.Number.ToLower().Contains(request.Number.ToLower()));
            }

            return _mapper.Map<List<SectionDto>>(await sections.ToListAsync(cancellationToken));
        }
    }
}
