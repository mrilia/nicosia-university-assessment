using System;
using System.Collections.Generic;

namespace Nicosia.Assessment.Domain.Models.User
{
    public class Lecturer
    {
        public Guid LecturerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SocialInsuranceNumber { get; set; }

        public virtual ICollection<Section.Section> Sections { get; set; } = null;

    }
}