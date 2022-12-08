using System;
using System.Collections.Generic;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;
using Nicosia.Assessment.Domain.Models.Security;

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
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null;
        public virtual ICollection<ApprovalRequest> ApprovalRequests { get; set; } = null;

    }
}