using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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
    public class GetClassListForLecturerQuery : PaginationRequest, IRequest<CollectionItems<ClassReportDto>>
    {
        public void SetLecturerId(Guid lecturerId)
        {
            _lecturerId = lecturerId;
        }
        
        public Guid GetLecturerId()
        {
            return _lecturerId;
        }

        private Guid _lecturerId { get; set; }
        public string Period { get; set; }
        public bool OnlyMyClasses { get; set; }
        public ClassSort Sort { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClassSort
    {
        None,
        ByClassNumberAsc,
        ByPeriodNameAsc,
        ByPeriodStartDateAsc,
    }


    public class GetClassListForLecturerQueryHandler : IRequestHandler<GetClassListForLecturerQuery, CollectionItems<ClassReportDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public GetClassListForLecturerQueryHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<ClassReportDto>> Handle(GetClassListForLecturerQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Section.Section> sections = _context.Sections
                                                                        .Include(i=>i.Period)
                                                                        .Include(i=>i.Course)
                                                                        .Include(i=>i.Lecturers);

            if (request.OnlyMyClasses)
            {
                sections = sections.Where(x => x.Lecturers!.Any(l=>l.LecturerId == request.GetLecturerId()));
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
