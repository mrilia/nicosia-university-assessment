using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Queries
{
    public class GetSectionByIdQuery : IRequest<Result<SectionDto>>
    {
        public Guid SectionId { get; set; }
    }

    public class GetSectionQueryHandler : IRequestHandler<GetSectionByIdQuery, Result<SectionDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public GetSectionQueryHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SectionDto>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections
                .Include(i=>i.Period)
                .Include(i=>i.Course)
                .Include(i=>i.Lecturers)
                .Include(i=>i.Students)
                .SingleOrDefaultAsync(x => x.SectionId == request.SectionId,
                cancellationToken);

            if (section is null)
                return Result<SectionDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.SectionNotFound)));

            RemoveUselessChildrenFromResult(section);

            return Result<SectionDto>.SuccessFul(_mapper.Map<SectionDto>(section));
        }

        //todo: refctor to put this cod into auto mapper
        private void RemoveUselessChildrenFromResult(Domain.Models.Section.Section section)
        {
            section!.Course!.Sections = null;
            section!.Period!.Sections = null;

            foreach (var student in section!.Students)
            {
                student!.Sections = null;
            }

            foreach (var lecturer in section!.Lecturers)
            {
                lecturer!.Sections = null;
            }
        }
    }
}
