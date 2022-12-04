using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.AddNewCourse
{
    public class AddNewCourseCommandHandler : IRequestHandler<AddNewCourseCommand, Result<CourseDto>>
    {
        private readonly ICourseContext _context;
        private readonly IMapper _mapper;

        public AddNewCourseCommandHandler(ICourseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(AddNewCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToAdd = _mapper.Map<Domain.Models.Course.Course>(request);

            await _context.Courses.AddAsync(courseToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<CourseDto>.SuccessFul(_mapper.Map<CourseDto>(courseToAdd));
        }
    }
}
