using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Period.Commands.AddNewPeriod;
using Nicosia.Assessment.Application.Handlers.Period.Commands.UpdatePeriod;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Domain.Models.Period;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class PeriodMappingProfile : Profile
    {
        public PeriodMappingProfile()
        {
            CreateMap<Period, PeriodDto>();
            CreateMap<AddNewPeriodCommand, Period>();
            CreateMap<UpdatePeriodCommand, Period>();
            CreateMap<Period, ChildlessPeriodDto>();

        }
    }
}
