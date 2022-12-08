using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Encryption;

namespace Nicosia.Assessment.Application.Validators.Admin
{
    public class AuthenticateAdminCommandValidator : AbstractValidator<AuthenticateAdminCommand>
    {
        private readonly IAdminContext _context;

        public AuthenticateAdminCommandValidator(IAdminContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage(ResponseMessage.UsernameIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.UsernameIsRequired).WithErrorCode("999");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage(ResponseMessage.PasswordIsRequired)
                .NotNull().WithMessage(ResponseMessage.PasswordIsRequired);
            
            RuleFor(dto => dto)
                .Must(AdminExists).WithMessage(ResponseMessage.UsernamePasswordInvalid);
        }
        
        private bool AdminExists(AuthenticateAdminCommand adminToCheck)
        {
            var admin = _context.Admins.SingleOrDefault(x =>
                x.Email.ToLower().Trim() == adminToCheck.Username.ToLower().Trim());
            
            if (admin == null || !new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Check(admin.Password, adminToCheck.Password).Verified)
                return false;

            return true;
        }
    }
}
