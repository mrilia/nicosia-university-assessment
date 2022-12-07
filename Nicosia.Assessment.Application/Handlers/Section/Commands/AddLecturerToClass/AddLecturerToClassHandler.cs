using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.AddLecturerToClass
{
    public class AddLecturerToClassHandler : IRequestHandler<AddLecturerToClassCommand, Result<ClassDto>>
    {
        private readonly ISectionContext _sectionContext;
        private readonly ILecturerContext _lecturerContext;
        private readonly IMapper _mapper;

        public AddLecturerToClassHandler(ISectionContext sectionContext, ILecturerContext lecturerContext, IMapper mapper)
        {
            _sectionContext = sectionContext;
            _lecturerContext = lecturerContext;
            _mapper = mapper;
        }

        public async Task<Result<ClassDto>> Handle(AddLecturerToClassCommand request, CancellationToken cancellationToken)
        {
            var sectionToUpdate = await GetSectionAsync(request, cancellationToken);

            foreach (var lecturerId in request.LecturerIds!)
            {
                var lecturerToAdd = await GetLecturerAsync(lecturerId, cancellationToken);
                sectionToUpdate.Lecturers.Add(lecturerToAdd);
            }

            await _sectionContext.SaveAsync(cancellationToken);

            RemoveUselessChildrenFromResult(sectionToUpdate);

            return Result<ClassDto>.SuccessFul(_mapper.Map<ClassDto>(sectionToUpdate));
        }

        //todo: refctor to put this cod into auto mapper
        private void RemoveUselessChildrenFromResult(Domain.Models.Section.Section sectionToUpdate)
        {
            sectionToUpdate!.Course.Sections = null;
            sectionToUpdate!.Period.Sections = null;

            foreach (var lecturer in sectionToUpdate.Lecturers)
            {
                lecturer!.Sections = null;
            }

            foreach (var lecturer in sectionToUpdate.Lecturers)
            {
                lecturer!.Sections = null;
            }
        }

        private async Task<Domain.Models.Section.Section> GetSectionAsync(AddLecturerToClassCommand request, CancellationToken cancellationToken)
        {
            return await _sectionContext.Sections
                .Include(i=>i.Course)
                .Include(i=>i.Period)
                .Include(i=>i.Lecturers)
                .Include(i=>i.Lecturers)
                .SingleOrDefaultAsync(x => x.SectionId == request.SectionId, cancellationToken);
        }

        private async Task<Domain.Models.User.Lecturer> GetLecturerAsync(Guid lecturerId, CancellationToken cancellationToken)
        {
            return await _lecturerContext.Lecturers.SingleOrDefaultAsync(x => x.LecturerId == lecturerId, cancellationToken);
        }
    }
}
