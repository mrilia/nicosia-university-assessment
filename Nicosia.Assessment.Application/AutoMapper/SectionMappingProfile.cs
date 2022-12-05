using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Section.Commands.AddNewSection;
using Nicosia.Assessment.Application.Handlers.Section.Commands.UpdateSection;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Domain.Models.Section;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class SectionMappingProfile : Profile
    {
        private readonly IPeriodContext _periodContext;
        private readonly ICourseContext _courseContext;
        public SectionMappingProfile(IPeriodContext periodContext, ICourseContext courseContext)
        {
            _periodContext = periodContext;
            _courseContext = courseContext;

            CreateMap<Section, SectionDto>()
                .ForMember(x => x.PeriodId, opt => opt.MapFrom(src => src.Period.PeriodId))
                .ForMember(x => x.CourseId, opt => opt.MapFrom(src => src.Course.CourseId));

            CreateMap<SectionDto, Section>()
                .ForMember(x => x.Period, opt => opt.MapFrom(src => _periodContext.Periods.Find(src.PeriodId)))
                .ForMember(x => x.Course, opt => opt.MapFrom(src => _courseContext.Courses.Find(src.CourseId)));

            CreateMap<AddNewSectionCommand, Section>();
            CreateMap<UpdateSectionCommand, Section>();
        }
    }
}
