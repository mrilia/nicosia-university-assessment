using System;
using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Section.Commands.UpdateSection;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Section
{
    public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IPeriodContext _periodContext;
        private readonly ICourseContext _courseContext;

        public UpdateSectionCommandValidator(ISectionContext sectionContext, IPeriodContext periodContext, ICourseContext courseContext)
        {
            _sectionContext = sectionContext;
            _periodContext = periodContext;
            _courseContext = courseContext;

            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Number)
                .NotEmpty().WithMessage(ResponseMessage.NumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.NumberIsRequired);

            RuleFor(dto => dto.PeriodId)
                .NotEmpty().WithMessage(ResponseMessage.PeriodIdIsRequired)
                .NotNull().WithMessage(ResponseMessage.PeriodIdIsRequired)
                .Must(PeriodExists).WithMessage(ResponseMessage.PeriodNotFound);

            RuleFor(dto => dto.CourseId)
                .NotEmpty().WithMessage(ResponseMessage.CourseIdIsRequired)
                .NotNull().WithMessage(ResponseMessage.CourseIdIsRequired)
                .Must(CourseExists).WithMessage(ResponseMessage.CourseNotFound);
        }

        private bool PeriodExists(Guid periodIdToCheck)
        {
            return _periodContext.Periods.Any(x => x.PeriodId == periodIdToCheck);
        }

        private bool CourseExists(Guid courseIdToCheck)
        {
            return _courseContext.Courses.Any(x => x.CourseId == courseIdToCheck);
        }
    }
}
