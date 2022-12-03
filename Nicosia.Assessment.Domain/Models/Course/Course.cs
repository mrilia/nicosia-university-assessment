using System;
using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.Course
{
    public class Course
    {
        public Course(ICollection<Section.Section> sections)
        {
            Sections = sections;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Section.Section> Sections { get; }
    }
}