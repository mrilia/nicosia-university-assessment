using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.UpdateLecturer
{
    public class UpdateLecturerCommand : IRequest<Result>
    {
        public Guid LecturerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
