using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Course.Queries
{
    public class GetCourseListQuery : IRequest<List<CourseDto>>
    {
        public string Code { get; set; }
    }


    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, List<CourseDto>>
    {
        private readonly ICourseContext _context;
        private readonly IMapper _mapper;

        public GetCourseListQueryHandler(ICourseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CourseDto>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Course.Course> Courses = _context.Courses;

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                Courses = Courses.Where(x => x.Code.ToLower().Contains(request.Code.ToLower()));
            }

            return _mapper.Map<List<CourseDto>>(await Courses.ToListAsync(cancellationToken));
        }
    }
}
