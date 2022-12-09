using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.User.V1.Authenticate.Admin
{
    [Route("api/user/v1/[controller]/Admin")]
    public class AuthenticateController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Tags = new[] { "Authentication Admin", "Major Assessment Endpoints" })]
        public IActionResult Authenticate(AuthenticateAdminCommand authenticateAdminCommand, CancellationToken cancellationToken)
        {
            authenticateAdminCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateAdminCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("refresh-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Admin" })]
        public IActionResult RefreshToken(RefreshAdminTokenCommand refreshAdminTokenCommand, CancellationToken cancellationToken)
        {
            refreshAdminTokenCommand.IpAdress = IpAddress();
            refreshAdminTokenCommand.RefreshToken =
                refreshAdminTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            var response = _mediator.Send(refreshAdminTokenCommand, cancellationToken).Result.Data;

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Admin" })]
        public async Task<IActionResult> RevokeToken(RevokeAdminTokenCommand revokeAdminTokenCommand, CancellationToken cancellationToken)
        {
            // accept refresh token in request body or cookie
            var token = revokeAdminTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            await _mediator.Send(revokeAdminTokenCommand, cancellationToken);

            return Ok(new { message = "Token revoked" });
        }

    }
}
