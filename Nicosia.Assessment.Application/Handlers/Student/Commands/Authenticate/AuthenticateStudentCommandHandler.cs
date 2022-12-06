using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Shared.Token.JWT;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate
{
    public class AuthenticateStudentCommandHandler : IRequestHandler<AuthenticateStudentCommand, Result<AuthenticateStudentResponse>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticateStudentCommandHandler(IStudentContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticateStudentResponse>> Handle(AuthenticateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _context.Students
                .Include(i=>i.RefreshTokens)
                .SingleOrDefault(s =>
                    s.Email.ToLower().Trim() == request.Username.ToLower().Trim());
            
            var jwtToken = JwtTokenHelper.GenerateJwtToken(student.StudentId,"student", _jwtSettings.Secret);
            var jwtRefreshToken = JwtTokenHelper.GenerateRefreshToken(request.IpAdress);

            student.RefreshTokens.Add(_mapper.Map<RefreshToken>(jwtRefreshToken));

            // remove old refresh tokens from user
            student.RefreshTokens.ToList().RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            await _context.SaveAsync(cancellationToken);
            
            return Result<AuthenticateStudentResponse>.SuccessFul(_mapper.Map<AuthenticateStudentResponse>(new AuthenticateResponse(student, jwtToken, jwtRefreshToken.Token)));
        }
    }
}
