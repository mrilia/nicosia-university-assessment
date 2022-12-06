using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Course.Queries
{
    public class GetCourseListQuery : PaginationRequest, IRequest<CollectionItems<CourseDto>>
    {
        public string Code { get; set; }
        public CourseSort Sort { get; set; }

    }
    public enum CourseSort
    {
        None,
        ByCodeAsc,
        ByTitleAsc
    }


    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, CollectionItems<CourseDto>>
    {
        private readonly ICourseContext _context;
        private readonly IMapper _mapper;

        public GetCourseListQueryHandler(ICourseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<CourseDto>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Course.Course> courses = _context.Courses;

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                courses = courses.Where(x => x.Code.ToLower().Contains(request.Code.ToLower()));
            }

            courses = ApplySort(courses, request.Sort);
            var totalCount = await courses.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<CourseDto>>(await courses.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<CourseDto>(result, totalCount);
        }

        private static IQueryable<Domain.Models.Course.Course> ApplySort(IQueryable<Domain.Models.Course.Course> query, CourseSort sort = CourseSort.None)
        {
            query = sort switch
            {
                CourseSort.ByCodeAsc => query.OrderBy(r => r.Code),
                CourseSort.ByTitleAsc => query.OrderBy(r => r.Title),
                _ => query
            };

            return query;
        }
    }
}
