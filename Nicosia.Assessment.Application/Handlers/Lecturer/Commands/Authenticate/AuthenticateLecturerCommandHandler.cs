using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate
{
    public class AuthenticateLecturerCommandHandler : IRequestHandler<AuthenticateLecturerCommand, Result<AuthenticateLecturerResponse>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticateLecturerCommandHandler(ILecturerContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticateLecturerResponse>> Handle(AuthenticateLecturerCommand request, CancellationToken cancellationToken)
        {
            var lecturer = _context.Lecturers
                .Include(i=>i.RefreshTokens)
                .SingleOrDefault(s =>
                    s.Email.ToLower().Trim() == request.Username.ToLower().Trim());
            
            var jwtToken = JwtTokenHelper.GenerateJwtToken(lecturer.LecturerId,"lecturer", _jwtSettings.Secret);
            var jwtRefreshToken = JwtTokenHelper.GenerateRefreshToken(request.IpAdress);

            lecturer.RefreshTokens.Add(_mapper.Map<RefreshToken>(jwtRefreshToken));

            // remove old refresh tokens from user
            lecturer.RefreshTokens.ToList().RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            await _context.SaveAsync(cancellationToken);
            
            return Result<AuthenticateLecturerResponse>.SuccessFul(_mapper.Map<AuthenticateLecturerResponse>(new AuthenticateResponse(lecturer, jwtToken, jwtRefreshToken.Token)));
        }
    }
}
