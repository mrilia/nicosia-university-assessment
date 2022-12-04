using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.UpdateSection
{
    public class UpdateSectionCommand : IRequest<Result>
    {
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }

        public Guid PeriodId { get; set; }
        public Guid CourseId { get; set; }

    }
}
