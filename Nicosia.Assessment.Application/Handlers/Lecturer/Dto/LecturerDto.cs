using Nicosia.Assessment.Domain.Models.Security;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Dto
{
    public class LecturerDto
    {
        public Guid LecturerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null;
    }
}
