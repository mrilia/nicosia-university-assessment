using System;
using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.Period
{
    public class Period
    {
        public Period(ICollection<Section.Section> sections)
        {
            Sections = sections;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public virtual ICollection<Section.Section> Sections { get; }
    }
}