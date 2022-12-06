using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate
{
    public class RevokeStudentTokenHandler : IRequestHandler<RevokeStudentTokenCommand, Result>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;

        public RevokeStudentTokenHandler(IStudentContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _mediator = mediator;
        }

        public async Task<Result> Handle(RevokeStudentTokenCommand request, CancellationToken cancellationToken)
        {
            var studentDto = _mediator.Send(new GetStudentByRefreshTokenQuery() { RefreshToken = request.RefreshToken }, cancellationToken).Result.Data;
            var refreshToken = studentDto.RefreshTokens.SingleOrDefault(s => s.Token == request.RefreshToken);

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
