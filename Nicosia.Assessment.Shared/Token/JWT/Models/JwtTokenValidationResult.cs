using System;

namespace Nicosia.Assessment.Shared.Token.JWT.Models
{
    public class JwtTokenValidationResult
    {
        public Guid UserId { get; set; }
        public string UserRole { get; set; }
    }
}
