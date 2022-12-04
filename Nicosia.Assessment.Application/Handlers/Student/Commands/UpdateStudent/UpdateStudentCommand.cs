using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Result>
    {
        public Guid StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
