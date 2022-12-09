using System;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ChildlessPeriodDto
    {
        public Guid PeriodId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}