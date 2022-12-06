using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate
{
    public class RevokeStudentTokenCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
        
        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
