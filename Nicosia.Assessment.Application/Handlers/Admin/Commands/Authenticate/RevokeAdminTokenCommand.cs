using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class RevokeAdminTokenCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
        
        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
