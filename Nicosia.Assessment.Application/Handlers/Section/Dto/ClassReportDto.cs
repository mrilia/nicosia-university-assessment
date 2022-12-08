using System.Collections.Generic;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassReportDto : ChildlessClassDto
    {
        public virtual ChildlessPeriodDto Period { get; set; }
        public virtual ChildlessCourseDto Course { get; set; }

        public virtual ICollection<ChildlessLecturerDto> Lecturers { get; set; } = null;
    }
}
