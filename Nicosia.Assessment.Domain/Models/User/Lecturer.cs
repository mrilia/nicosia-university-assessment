using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.User
{
    public class Lecturer: User
    {
        public Lecturer(ICollection<Section.Section> classes)
        {
            Classes = classes;
        }

        public string SocialInsuranceNumber { get; set; }
        public virtual ICollection<Section.Section> Classes { get; }

    }
}