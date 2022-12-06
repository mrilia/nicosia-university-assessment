using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate
{
    public class AuthenticateStudentCommand : IRequest<Result<AuthenticateStudentResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
