using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate
{
    public class AuthenticateLecturerCommand : IRequest<Result<AuthenticateLecturerResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
