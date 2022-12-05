using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Domain.Models.Security;

namespace Nicosia.Assessment.Application.Validators.Admin
{
    public class RefreshAdminTokenCommandValidator : AbstractValidator<RefreshAdminTokenCommand>
    {
        private readonly IAdminContext _context;

        public RefreshAdminTokenCommandValidator(IAdminContext context)
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
        
        private bool TokenExists(RefreshAdminTokenCommand tokenToCheck)
        {
            return _context.Admins.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.RefreshToken));
        }

        private bool TokenBeActive(RefreshAdminTokenCommand tokenToCheck)
        {
            var admin =  _context.Admins
                .Include(i=>i.RefreshTokens)
                .SingleOrDefault(x => x.RefreshTokens.Any(s =>s.Token == tokenToCheck.RefreshToken));
            if (admin is null)
            {
                return false;
            }

            var refreshToken = admin.RefreshTokens.SingleOrDefault(s => s.Token == tokenToCheck.RefreshToken);
            return refreshToken is not null && refreshToken.IsActive;
        }
    }
}
