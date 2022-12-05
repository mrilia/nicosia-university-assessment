using MediatR;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class RevokeAdminTokenCommand : IRequest<Result>
    {
        public string Token { get; set; }
        public string IpAdress { get; set; }
    }
}
