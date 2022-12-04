using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.UpdateSection
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Result>
    {
        private readonly ISectionContext _context;
        private readonly IMapper _mapper;

        public UpdateSectionCommandHandler(ISectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var sectionToUpdate = await GetSectionAsync(request, cancellationToken);

            if (sectionToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.SectionNotFound)));

            _mapper.Map(request, sectionToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Section.Section> GetSectionAsync(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            return await _context.Sections.SingleOrDefaultAsync(x => x.SectionId == request.SectionId, cancellationToken);
        }

    }
}
