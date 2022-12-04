using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.UpdatePeriod
{
    public class UpdatePeriodCommand : IRequest<Result>
    {
        public Guid PeriodId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
