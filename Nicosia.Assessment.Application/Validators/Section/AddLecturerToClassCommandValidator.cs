using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Commands.AddLecturerToClass;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Section
{
    public class AddLecturerToClassCommandValidator : AbstractValidator<AddLecturerToClassCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly ILecturerContext _lecturerContext;

        public AddLecturerToClassCommandValidator(ISectionContext sectionContext, ILecturerContext lecturerContext)
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
                .Must(LecturerNotAddedBeforeToThisClass).WithMessage(ResponseMessage.LecturerAddedBeforToClass);
        }

        private bool LecturerNotAddedBeforeToThisClass(AddLecturerToClassCommand addLecturerToClassCommand)
        {
            var section = _sectionContext.Sections
                                         .Include(i => i.Lecturers)
                                         .FirstOrDefault(x => x.SectionId == addLecturerToClassCommand.SectionId);

            if (section == null)
                throw new InvalidDataException(ResponseMessage.SectionNotFound);

            if (section.Lecturers == null)
                return true;

            return addLecturerToClassCommand!.LecturerIds!.All(a => section.Lecturers.All(d => d.LecturerId != a));
        }

        private bool LecturerExists(List<Guid> lecturerIdsToCheck)
        {
            return lecturerIdsToCheck?.Any(a => _lecturerContext.Lecturers.Any(x => x.LecturerId == a)) ?? false;
        }

        private bool SectionExists(Guid sectionIdToCheck)
        {
            return _sectionContext.Sections.Any(x => x.SectionId == sectionIdToCheck);
        }

    }
}
