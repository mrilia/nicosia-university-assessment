using System;
using System.Text.Json.Serialization;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Domain.Models.Security
{
    public class RefreshToken
    {
        [JsonIgnore]
        public Guid RefreshTokenId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public string ReasonRevoked { get; set; }

        public Guid? AdminId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? LecturerId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Student Student { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
