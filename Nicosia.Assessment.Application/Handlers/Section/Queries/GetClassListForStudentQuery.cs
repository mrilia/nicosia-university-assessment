using System;
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
    public class GetClassListForStudentQuery : PaginationRequest, IRequest<CollectionItems<ClassReportDto>>
    {
        public void SetStudentId(Guid studentId)
        {
            _studentId = studentId;
        }
        
        public Guid GetStudentId()
        {
            return _studentId;
        }
        private Guid _studentId { get; set; }
        public string Period { get; set; }
        public bool OnlyMyClasses { get; set; }
        public ClassSort Sort { get; set; }
    }


    public class GetClassListForStudentQueryHandler : IRequestHandler<GetClassListForStudentQuery, CollectionItems<ClassReportDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public GetClassListForStudentQueryHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<ClassReportDto>> Handle(GetClassListForStudentQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Section.Section> sections = _context.Sections
                                                                        .Include(i=>i.Period)
                                                                        .Include(i=>i.Course)
                                                                        .Include(i=>i.Lecturers);

            if (request.OnlyMyClasses)
            {
                sections = sections.Where(x => x.Students!.Any(l=>l.StudentId == request.GetStudentId()));
            }

            if (!string.IsNullOrWhiteSpace(request.Period))
            {
                sections = sections.Where(x => x.Period!.Name.ToLower().Contains(request.Period.ToLower()));
            }

            sections = ApplySort(sections, request.Sort);
            var totalCount = await sections.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<ClassReportDto>>(await sections.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<ClassReportDto>(result, totalCount);
        }

        private static IQueryable<Domain.Models.Section.Section> ApplySort(IQueryable<Domain.Models.Section.Section> query, ClassSort sort = ClassSort.None)
        {
            query = sort switch
            {
                ClassSort.ByClassNumberAsc => query.OrderBy(r => r.Number),
                ClassSort.ByPeriodNameAsc => query.OrderBy(r => r.Period!.Name),
                ClassSort.ByPeriodStartDateAsc => query.OrderBy(r => r.Period!.StartDate),
                _ => query
            };

            return query;
        }
    }
}
