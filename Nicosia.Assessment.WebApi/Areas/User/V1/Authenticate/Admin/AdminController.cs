using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters.Swagger.DocumentFilters;
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

        /// <summary>
        /// Authenticate Admin user
        /// </summary>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Tags = new[] { "Authentication Admin", "Major Assessment Endpoints" })]
        [SwaggerOperationOrder(1)]
        public IActionResult Authenticate(AuthenticateAdminCommand authenticateAdminCommand, CancellationToken cancellationToken)
        {
            authenticateAdminCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateAdminCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token For Admin user
        /// </summary>
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

        /// <summary>
        /// Revoke Token For Admin user
        /// </summary>
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
