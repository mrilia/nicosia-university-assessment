using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.DeleteSection
{
    public class DeleteSectionCommand : IRequest<Result>
    {
        public Guid SectionId { get; set; }
    }
}
