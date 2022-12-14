using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class AuthenticateAdminCommandHandler : IRequestHandler<AuthenticateAdminCommand, Result<AuthenticateAdminResponse>>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticateAdminCommandHandler(IAdminContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticateAdminResponse>> Handle(AuthenticateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = _context.Admins
                .Include(i=>i.RefreshTokens)
                .SingleOrDefault(s =>
                    s.Email.ToLower().Trim() == request.Username.ToLower().Trim());
            
            var jwtToken = JwtTokenHelper.GenerateJwtToken(admin.AdminId,"admin", _jwtSettings.Secret);
            var jwtRefreshToken = JwtTokenHelper.GenerateRefreshToken(request.IpAdress);

            admin.RefreshTokens.Add(_mapper.Map<RefreshToken>(jwtRefreshToken));

            // remove old refresh tokens from user
            admin.RefreshTokens.ToList().RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            await _context.SaveAsync(cancellationToken);
            
            return Result<AuthenticateAdminResponse>.SuccessFul(_mapper.Map<AuthenticateAdminResponse>(new AuthenticateResponse(admin, jwtToken, jwtRefreshToken.Token)));
        }
    }
}
