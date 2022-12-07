using System;
using System.Collections.Generic;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.WithdrawLecturerToClass
{
    public class WithdrawLecturerFromClassCommand : IRequest<Result<ClassDto>>
    {
        public Guid SectionId { get; set; }
        public List<Guid> LecturerIds { get; set; }
    }
}
