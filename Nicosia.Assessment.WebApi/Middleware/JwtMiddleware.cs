using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Nicosia.Assessment.Shared.Token.JWT;
using Nicosia.Assessment.Shared.Token.JWT.Models;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Admin.Queries;
using System.Threading;
using Nicosia.Assessment.Application.Handlers.Lecturer.Queries;
using Nicosia.Assessment.Application.Handlers.Student.Queries;

namespace Nicosia.Assessment.WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> appSettings, IMediator mediator)
        {
            _next = next;
            _jwtSettings = appSettings.Value;
            _mediator = mediator;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var jwtTokenValidationResult = JwtTokenHelper.ValidateJwtToken(token, _jwtSettings.Secret);
            if (jwtTokenValidationResult != null)
            {
                switch (jwtTokenValidationResult.UserRole.Trim().ToLower())
                {
                    case "admin":
                        context.Items["User"] = _mediator.Send(new GetAdminByIdQuery { AdminId = jwtTokenValidationResult.UserId }).Result.Data;
                        break;
                    case "lecturer":
                        context.Items["User"] = _mediator.Send(new GetLecturerByIdQuery{ LecturerId = jwtTokenValidationResult.UserId }).Result.Data;
                        break;
                    case "student":
                        context.Items["User"] = _mediator.Send(new GetStudentQuery() { StudentId = jwtTokenValidationResult.UserId }).Result.Data;
                        break;
                }
                // attach user to context on successful jwt validation
            }

            await _next(context);
        }
    }
}
