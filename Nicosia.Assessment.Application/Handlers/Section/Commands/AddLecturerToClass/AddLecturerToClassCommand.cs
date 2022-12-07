using System;
using System.Collections.Generic;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.AddLecturerToClass
{
    public class AddLecturerToClassCommand : IRequest<Result<ClassDto>>
    {
        public Guid SectionId { get; set; }
        public List<Guid> LecturerIds { get; set; }
    }
}
