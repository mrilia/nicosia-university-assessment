using System;

namespace Nicosia.Assessment.Application.Handlers.Period.Dto
{
    public class PeriodDto
    {
        public Guid PeriodId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
