using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
