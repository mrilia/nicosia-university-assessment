using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Queries
{
    public class GetCourseQuery : IRequest<Result<CourseDto>>
    {
        public Guid CourseId { get; set; }
    }

    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, Result<CourseDto>>
    {
        private readonly ICourseContext _context;
        private readonly IMapper _mapper;

        public GetCourseQueryHandler(ICourseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var Course = await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == request.CourseId,
                cancellationToken);

            if (Course is null)
                return Result<CourseDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CourseNotFound)));

            return Result<CourseDto>.SuccessFul(_mapper.Map<CourseDto>(Course));
        }
    }
}
