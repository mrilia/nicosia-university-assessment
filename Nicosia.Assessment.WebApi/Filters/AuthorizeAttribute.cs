using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Nicosia.Assessment.Domain.Models.User;
using Nicosia.Assessment.Shared.Token.JWT.Models;
using Nicosia.Assessment.Shared.Token.JWT;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Nicosia.Assessment.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NicosiaAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;
        private JwtSettings _jwtSettings;

        public NicosiaAuthorizeAttribute(string roles)
        {
            _roles = roles?.Split(',') ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _jwtSettings = context.HttpContext.RequestServices.GetService<IOptions<JwtSettings>>()!.Value;

            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var jwtTokenValidationResult = JwtTokenHelper.ValidateJwtToken(token, _jwtSettings.Secret);

            // authorization
            if (jwtTokenValidationResult is null 
                || jwtTokenValidationResult.UserId == Guid.Empty
                || (_roles.Any() && !_roles.Select(s=> s.ToLower()).Contains(jwtTokenValidationResult.UserRole.ToLower())))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
