using System;

namespace Nicosia.Assessment.Application.Handlers.Student.Dto
{
    public class AuthenticateStudentResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
