using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.DeleteLecturer
{
    public class DeleteLecturerCommand : IRequest<Result>
    {
        public Guid LecturerId { get; set; }
    }
}
