using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Admin.Queries;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate
{
    public class RevokeAdminTokenHandler : IRequestHandler<RevokeAdminTokenCommand, Result>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;

        public RevokeAdminTokenHandler(IAdminContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _mediator = mediator;
        }

        public async Task<Result> Handle(RevokeAdminTokenCommand request, CancellationToken cancellationToken)
        {
            var adminDto = _mediator.Send(new GetAdminByRefreshTokenQuery() { RefreshToken = request.Token }, cancellationToken).Result.Data;
            var refreshToken = adminDto.RefreshTokens.SingleOrDefault(s => s.Token == request.Token);

            // replace old refresh token with a new one (rotate token)
            RevokeRefreshToken(refreshToken, request.IpAdress, "Revoked without replacement");

            // save changes to db
            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
