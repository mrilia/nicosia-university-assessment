using System;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class SectionDto
    {
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }

        public Guid PeriodId { get; set; }
        public Guid CourseId { get; set; }
    }
}
