using System;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Shared.Token.JWT.Models
{
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(Admin admin, string jwtToken, string refreshToken)
        {
            UserId = admin.AdminId;
            FirstName = admin.Firstname;
            LastName = admin.Lastname;
            Username = admin.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
