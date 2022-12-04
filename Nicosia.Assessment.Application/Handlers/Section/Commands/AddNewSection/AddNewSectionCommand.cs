using MediatR;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Results;
using System;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.AddNewSection
{
    public class AddNewSectionCommand : IRequest<Result<SectionDto>>
    {
        public string Number { get; set; }
        public string Details { get; set; }

        public Guid PeriodId { get; set; }
        public Guid CourseId { get; set; }
    }
}
