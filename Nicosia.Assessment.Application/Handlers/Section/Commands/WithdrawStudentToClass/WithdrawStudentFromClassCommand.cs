using System;
using System.Collections.Generic;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.WithdrawStudentToClass
{
    public class WithdrawStudentFromClassCommand : IRequest<Result<ClassDto>>
    {
        public Guid SectionId { get; set; }
        public List<Guid> StudentIds { get; set; }
    }
}
