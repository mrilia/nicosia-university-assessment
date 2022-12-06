using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
