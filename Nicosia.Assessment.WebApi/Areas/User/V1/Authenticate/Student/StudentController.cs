using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters.Swagger.DocumentFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.User.V1.Authenticate.Student
{
    [Route("api/user/v1/[controller]/student")]
    public class AuthenticateController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Authenticate Student user
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Tags = new[] { "Authentication Student", "Major Assessment Endpoints" })]
        [SwaggerOperationOrder(1)]
        public IActionResult Authenticate(AuthenticateStudentCommand authenticateStudentCommand, CancellationToken cancellationToken)
        {
            authenticateStudentCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateStudentCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token For Student user
        /// </summary>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Student" })]
        public IActionResult RefreshToken(RefreshStudentTokenCommand refreshStudentTokenCommand, CancellationToken cancellationToken)
        {
            refreshStudentTokenCommand.IpAdress = IpAddress();
            refreshStudentTokenCommand.RefreshToken =
                refreshStudentTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            var response = _mediator.Send(refreshStudentTokenCommand, cancellationToken).Result.Data;

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Revoke Token For Student user
        /// </summary>
        [HttpPost("revoke-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Student" })]
        public async Task<IActionResult> RevokeToken(RevokeStudentTokenCommand revokeStudentTokenCommand, CancellationToken cancellationToken)
        {
            // accept refresh token in request body or cookie
            var token = revokeStudentTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            await _mediator.Send(revokeStudentTokenCommand, cancellationToken);

            return Ok(new { message = "Token revoked" });
        }

    }
}
