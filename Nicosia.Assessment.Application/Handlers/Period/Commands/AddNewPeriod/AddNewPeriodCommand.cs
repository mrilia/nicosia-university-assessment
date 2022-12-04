using MediatR;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Results;
using System;

namespace Nicosia.Assessment.Application.Handlers.Period.Commands.AddNewPeriod
{
    public class AddNewPeriodCommand : IRequest<Result<PeriodDto>>
    {
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
