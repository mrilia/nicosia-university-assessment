using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate
{
    public class RefreshLecturerTokenCommand : IRequest<Result<AuthenticateLecturerResponse>>
    {
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
