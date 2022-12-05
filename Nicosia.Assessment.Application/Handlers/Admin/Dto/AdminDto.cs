using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Nicosia.Assessment.Domain.Models.Security;

namespace Nicosia.Assessment.Application.Handlers.Admin.Dto
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null;
    }
}
