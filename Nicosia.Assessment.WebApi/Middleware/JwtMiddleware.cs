using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Nicosia.Assessment.Security.Authorization;

namespace Nicosia.Assessment.WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUserContextPopulator userContextPopulator)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(userContextPopulator, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(IUserContextPopulator userContextPopulator, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecurityKey").Value);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var claimSet = GetClaimSet(jwtToken.Claims);
                userContextPopulator.Populate(claimSet);
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
        }

        private static ClaimSet GetClaimSet(IEnumerable<Claim> claims)
        {
            var claimSet = new ClaimSet();
            foreach (var claim in claims)
            {
                claimSet.Add(claim.Type, claim.Value);
            }

            return claimSet;
        }
    }
}
