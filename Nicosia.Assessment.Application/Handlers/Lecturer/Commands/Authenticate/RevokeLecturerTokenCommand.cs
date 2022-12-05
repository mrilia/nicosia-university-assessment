using System.Text.Json.Serialization;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate
{
    public class RevokeLecturerTokenCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
        
        [JsonIgnore]
        public string IpAdress { get; set; }
    }
}
