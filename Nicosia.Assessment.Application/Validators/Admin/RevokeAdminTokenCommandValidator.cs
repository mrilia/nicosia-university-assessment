using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Admin
{
    public class RevokeAdminTokenCommandValidator : AbstractValidator<RevokeAdminTokenCommand>
    {
        private readonly IAdminContext _context;

        public RevokeAdminTokenCommandValidator(IAdminContext context)
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
        
        private bool TokenExists(RevokeAdminTokenCommand tokenToCheck)
        {
            return _context.Admins.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
        }

        private bool TokenBeActive(RevokeAdminTokenCommand tokenToCheck)
        {
            var admin = _context.Admins
                .Include(i => i.RefreshTokens)
                .SingleOrDefault(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
            if (admin is null)
            {
                return false;
            }

            var refreshToken = admin.RefreshTokens.SingleOrDefault(s => s.Token == tokenToCheck.RefreshToken);
            return refreshToken is not null && refreshToken.IsActive;
        }
    }
}
