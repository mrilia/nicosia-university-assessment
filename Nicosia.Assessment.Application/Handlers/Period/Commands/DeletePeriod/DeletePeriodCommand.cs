using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.DeletePeriod
{
    public class DeletePeriodCommand : IRequest<Result>
    {
        public Guid PeriodId { get; set; }
    }
}
