using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Admin
{
    public class RefreshAdminTokenCommandValidator : AbstractValidator<RefreshAdminTokenCommand>
    {
        private readonly IAdminContext _context;

        public RefreshAdminTokenCommandValidator(IAdminContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Token)
                .NotEmpty().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.TokenIsRequired).WithErrorCode("999");

            RuleFor(dto => dto.IpAdress)
                .NotEmpty().WithMessage(ResponseMessage.IpAdressIsRequired)
                .NotNull().WithMessage(ResponseMessage.IpAdressIsRequired);
            
            RuleFor(dto => dto)
                .Must(TokenExists).WithMessage(ResponseMessage.TokenNotFound)
                .Must(TokenBeActive).WithMessage(ResponseMessage.TokenIsNotActive);
        }
        
        private bool TokenExists(RefreshAdminTokenCommand tokenToCheck)
        {
            return _context.Admins.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.Token));
        }

        private bool TokenBeActive(RefreshAdminTokenCommand tokenToCheck)
        {
            return _context.Admins.Any(x => x.RefreshTokens.Any(s => s.Token == tokenToCheck.Token && s.IsActive));
        }
    }
}
