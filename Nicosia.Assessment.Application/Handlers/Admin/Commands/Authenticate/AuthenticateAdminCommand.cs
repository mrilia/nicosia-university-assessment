using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class AuthenticateAdminCommand : IRequest<Result<AuthenticateAdminResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
