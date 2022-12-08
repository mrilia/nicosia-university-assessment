using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassReportDto : ChildlessClassDto
    {
        public virtual ChildlessPeriodDto Period { get; set; }
        public virtual ChildlessCourseDto Course { get; set; }

        public virtual ICollection<ChildlessLecturerDto> Lecturers { get; set; } = null;
    }
}
