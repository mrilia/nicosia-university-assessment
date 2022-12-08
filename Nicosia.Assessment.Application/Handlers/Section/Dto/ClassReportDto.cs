using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassReportDto
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
