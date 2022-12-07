using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Student
{
    public class RefreshStudentTokenCommandValidator : AbstractValidator<RefreshStudentTokenCommand>
    {
        private readonly IStudentContext _context;

        public RefreshStudentTokenCommandValidator(IStudentContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.RefreshToken)
                .NotEmpty().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("999");
            
            RuleFor(dto => dto)
                .Must(TokenExists).WithMessage(ResponseMessage.TokenNotFound)
                .Must(TokenBeActive).WithMessage(ResponseMessage.TokenIsNotActive);

            RuleFor(dto => dto.IpAdress)
                .NotEmpty().WithMessage(ResponseMessage.IpAdressIsRequired)
                .NotNull().WithMessage(ResponseMessage.IpAdressIsRequired);
        }

        private bool TokenExists(RefreshStudentTokenCommand tokenToCheck)
        {
            return _context.Students.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
        }

        private bool TokenBeActive(RefreshStudentTokenCommand tokenToCheck)
        {
            var student =  _context.Students
                .Include(i=>i.RefreshTokens)
                .SingleOrDefault(x => x.RefreshTokens.Any(s =>s.Token == tokenToCheck.RefreshToken));
            if (student is null)
            {
                return false;
            }

            var refreshToken = student.RefreshTokens.SingleOrDefault(s => s.Token == tokenToCheck.RefreshToken);
            return refreshToken is not null && refreshToken.IsActive;
        }
    }
}
