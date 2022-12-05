using System;
using System.Collections.Generic;
using Nicosia.Assessment.Domain.Models.Security;

namespace Nicosia.Assessment.Domain.Models.User
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Section.Section> Sections { get; set; } = null;
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null;
    }
}