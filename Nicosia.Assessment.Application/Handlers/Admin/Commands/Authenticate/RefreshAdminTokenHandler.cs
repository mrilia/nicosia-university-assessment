using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Handlers.Admin.Queries;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class RefreshAdminTokenHandler : IRequestHandler<RefreshAdminTokenCommand, Result<AuthenticateAdminResponse>>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;

        public RefreshAdminTokenHandler(IAdminContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _mediator = mediator;
        }

        public async Task<Result<AuthenticateAdminResponse>> Handle(RefreshAdminTokenCommand request, CancellationToken cancellationToken)
        {
            var adminDto = _mediator.Send(new GetAdminByRefreshTokenQuery() { RefreshToken = request.RefreshToken }, cancellationToken).Result.Data;
            var refreshToken = adminDto.RefreshTokens.SingleOrDefault(s => s.Token == request.RefreshToken);

            if (refreshToken!.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                RevokeDescendantRefreshTokens(refreshToken, adminDto, request.IpAdress, $"Attempted reuse of revoked ancestor token: {request.RefreshToken}");

                await _context.SaveAsync(cancellationToken);
            }

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, request.IpAdress);
            adminDto.RefreshTokens.Add(_mapper.Map<RefreshToken>(newRefreshToken));
            
            // remove old refresh tokens from user
            adminDto.RefreshTokens.ToList().RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            // save changes to db
            await _context.SaveAsync(cancellationToken);
            
            
            var jwtToken = JwtTokenHelper.GenerateJwtToken(adminDto.AdminId,"admin", _jwtSettings.Secret);
            var jwtRefreshToken = JwtTokenHelper.GenerateRefreshToken(request.IpAdress);

            adminDto.RefreshTokens.Add(_mapper.Map<RefreshToken>(jwtRefreshToken));

            return Result<AuthenticateAdminResponse>.SuccessFul(_mapper.Map<AuthenticateAdminResponse>(new AuthenticateResponse(_mapper.Map<Domain.Models.User.Admin>(adminDto), jwtToken, jwtRefreshToken.Token)));
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, AdminDto user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private JwtRefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = JwtTokenHelper.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }
    }
}
