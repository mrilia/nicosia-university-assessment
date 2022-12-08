using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Nicosia.Assessment.Domain.Models.Course;
using Nicosia.Assessment.Domain.Models.Period;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassForLecturerDto
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }
        
        public virtual ChildlessPeriodDto Period { get; set; }
        public virtual ChildlessCourseDto Course { get; set; }

        public virtual ICollection<ChildlessLecturerDto> Lecturers { get; set; } = null;
    }
}
