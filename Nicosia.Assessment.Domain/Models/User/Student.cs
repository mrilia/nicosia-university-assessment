using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.User
{
    public class Student: User
    {
        public Student(ICollection<Section.Section> classes)
        {
            Classes = classes;
        }

        public virtual ICollection<Section.Section> Classes { get; }

    }
}