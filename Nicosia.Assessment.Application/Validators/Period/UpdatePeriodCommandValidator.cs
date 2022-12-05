using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Period.Commands.UpdatePeriod;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Period
{
    public class UpdatePeriodCommandValidator : AbstractValidator<UpdatePeriodCommand>
    {
        private readonly IPeriodContext _context;

        public UpdatePeriodCommandValidator(IPeriodContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage(ResponseMessage.NameIsRequired)
                .NotNull().WithMessage(ResponseMessage.NameIsRequired);
            
            RuleFor(dto => dto.StartDate)
                .NotEmpty().WithMessage(ResponseMessage.StartDateIsRequired)
                .NotNull().WithMessage(ResponseMessage.StartDateIsRequired);
            
            RuleFor(dto => dto.EndDate)
                .NotEmpty().WithMessage(ResponseMessage.EndDateIsRequired)
                .NotNull().WithMessage(ResponseMessage.EndDateIsRequired);

        }
    }
}
