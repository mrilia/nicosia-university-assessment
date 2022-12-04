using System;
using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.Course
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Section.Section> Sections { get; set; } = null;
    }
}