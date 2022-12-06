using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate;
using Nicosia.Assessment.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.User.V1.Authenticate.Lecturer
{
    [Route("api/user/v1/[controller]/lecturer")]
    public class AuthenticateController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Tags = new[] { "Authentication Lecturer" })]
        public IActionResult Authenticate(AuthenticateLecturerCommand authenticateLecturerCommand, CancellationToken cancellationToken)
        {
            authenticateLecturerCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateLecturerCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Lecturer" })]
        public IActionResult RefreshToken(RefreshLecturerTokenCommand refreshLecturerTokenCommand, CancellationToken cancellationToken)
        {
            refreshLecturerTokenCommand.IpAdress = IpAddress();
            refreshLecturerTokenCommand.RefreshToken =
                refreshLecturerTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            var response = _mediator.Send(refreshLecturerTokenCommand, cancellationToken).Result.Data;

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        [SwaggerOperation(Tags = new[] { "Authentication Lecturer" })]
        public async Task<IActionResult> RevokeToken(RevokeLecturerTokenCommand revokeLecturerTokenCommand, CancellationToken cancellationToken)
        {
            // accept refresh token in request body or cookie
            var token = revokeLecturerTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            await _mediator.Send(revokeLecturerTokenCommand, cancellationToken);

            return Ok(new { message = "Token revoked" });
        }

    }
}
