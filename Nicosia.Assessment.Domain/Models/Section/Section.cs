
using System;
using System.Collections.Generic;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Domain.Models.Section
{
    public class Section
    {
        public Section(ICollection<Lecturer> lecturers)
        {
            Lecturers = lecturers;
        }

        public Guid Id { get; set; }
        public virtual Period.Period Period { get; set; }
        public virtual Course.Course Course { get; set; }
        public string Details { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; }

    }
}