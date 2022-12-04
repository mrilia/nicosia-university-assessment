using MediatR;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.AddNewCourse
{
    public class AddNewCourseCommand : IRequest<Result<CourseDto>>
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
