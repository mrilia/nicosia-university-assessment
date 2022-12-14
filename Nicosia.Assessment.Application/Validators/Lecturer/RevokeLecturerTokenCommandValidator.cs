using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Lecturer
{
    public class RevokeLecturerTokenCommandValidator : AbstractValidator<RevokeLecturerTokenCommand>
    {
        private readonly ILecturerContext _context;

        public RevokeLecturerTokenCommandValidator(ILecturerContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.RefreshToken)
                .NotEmpty().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("999");

            RuleFor(dto => dto)
                .Must(TokenExists).WithMessage(ResponseMessage.TokenNotFound)
                .Must(TokenBeActive).WithMessage(ResponseMessage.TokenIsNotActive);
        }
        
        private bool TokenExists(RevokeLecturerTokenCommand tokenToCheck)
        {
            return _context.Lecturers.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
        }

        private bool TokenBeActive(RevokeLecturerTokenCommand tokenToCheck)
        {
            var lecturer = _context.Lecturers
                .Include(i => i.RefreshTokens)
                .SingleOrDefault(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
            if (lecturer is null)
            {
                return false;
            }

            var refreshToken = lecturer.RefreshTokens.SingleOrDefault(s => s.Token == tokenToCheck.RefreshToken);
            return refreshToken is not null && refreshToken.IsActive;
        }
    }
}
