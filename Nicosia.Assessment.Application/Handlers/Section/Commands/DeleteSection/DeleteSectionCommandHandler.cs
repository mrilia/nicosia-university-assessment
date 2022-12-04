using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.DeleteSection
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Result>
    {
        private readonly ISectionContext _context;

        public DeleteSectionCommandHandler(ISectionContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await GetSectionAsync(request, cancellationToken);

            if (section is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.SectionNotFound)));

            _context.Sections.Remove(section);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Section.Section> GetSectionAsync(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            return await _context.Sections.SingleOrDefaultAsync(x => x.SectionId == request.SectionId, cancellationToken);
        }

    }
}
