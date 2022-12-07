using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Commands.WithdrawLecturerToClass;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Section
{
    public class WithdrawLecturerFromClassCommandValidator : AbstractValidator<WithdrawLecturerFromClassCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly ILecturerContext _lecturerContext;

        public WithdrawLecturerFromClassCommandValidator(ISectionContext sectionContext, ILecturerContext lecturerContext)
        {
            _sectionContext = sectionContext;
            _lecturerContext = lecturerContext;

            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.SectionId)
                .NotEmpty().WithMessage(ResponseMessage.SectionIdIsRequired)
                .NotNull().WithMessage(ResponseMessage.SectionIdIsRequired)
                .Must(SectionExists).WithMessage(ResponseMessage.SectionNotFound);

            RuleFor(dto => dto.LecturerIds)
                .NotNull().WithMessage(ResponseMessage.LecturerIdIsRequired)
                .NotEmpty().WithMessage(ResponseMessage.LecturerIdIsRequired)
                .Must(LecturerExists).WithMessage(ResponseMessage.LecturerNotFound);

            RuleFor(dto => dto)
                .Must(LecturerAddedBeforeToThisClass).WithMessage(ResponseMessage.LecturerNotAddedBeforeToClass);
        }

        private bool LecturerAddedBeforeToThisClass(WithdrawLecturerFromClassCommand withdrawLecturerFromClassCommand)
        {
            var section = _sectionContext.Sections
                .Include(i => i.Lecturers)
                .FirstOrDefault(x => x.SectionId == withdrawLecturerFromClassCommand.SectionId);

            if (section == null)
                throw new InvalidDataException(ResponseMessage.SectionNotFound);

            if (section.Lecturers == null)
                return true;

            return withdrawLecturerFromClassCommand!.LecturerIds!.All(a => section.Lecturers.Any(d => d.LecturerId == a));
        }

        private bool LecturerExists(List<Guid> lecturerIdToCheck)
        {
            return lecturerIdToCheck?.Any(a => _lecturerContext.Lecturers.Any(x => x.LecturerId == a)) ?? false;
        }

        private bool SectionExists(Guid sectionIdToCheck)
        {
            return _sectionContext.Sections.Any(x => x.SectionId == sectionIdToCheck);
        }

    }
}
