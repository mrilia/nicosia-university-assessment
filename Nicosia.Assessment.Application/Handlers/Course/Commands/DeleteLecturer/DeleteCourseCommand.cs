using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.DeleteLecturer
{
    public class DeleteCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
    }
}
