using System;
using System.Collections.Generic;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassDto
    {
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }

        public virtual Domain.Models.Period.Period Period { get; set; }
        public virtual Domain.Models.Course.Course Course { get; set; }
        public virtual ICollection<Domain.Models.User.Lecturer> Lecturers { get; set; } = null;
        public virtual ICollection<Domain.Models.User.Student> Students { get; set; } = null;
    }
}
