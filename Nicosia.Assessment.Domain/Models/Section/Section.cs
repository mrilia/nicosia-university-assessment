
using System;
using System.Collections.Generic;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Domain.Models.Section
{
    public class Section
    {
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }
        public Guid PeriodId { get; set; }
        public Guid CourseId { get; set; }

        public virtual Period.Period Period { get; set; }
        public virtual Course.Course Course { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; set; } = null;
        public virtual ICollection<Student> Students { get; set; } = null;

    }
}