using System;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ChildlessPeriodDto
    {
        [JsonIgnore]
        public Guid PeriodId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}