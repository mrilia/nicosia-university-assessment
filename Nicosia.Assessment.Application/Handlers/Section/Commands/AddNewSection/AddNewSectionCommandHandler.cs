using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.AddNewSection
{
    public class AddNewSectionCommandHandler : IRequestHandler<AddNewSectionCommand, Result<SectionDto>>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public AddNewSectionCommandHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SectionDto>> Handle(AddNewSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sectionToAdd = _mapper.Map<Domain.Models.Section.Section>(request);

                await _context.Sections.AddAsync(sectionToAdd, cancellationToken);
                await _context.SaveAsync(cancellationToken);

                return Result<SectionDto>.SuccessFul(_mapper.Map<SectionDto>(sectionToAdd));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
