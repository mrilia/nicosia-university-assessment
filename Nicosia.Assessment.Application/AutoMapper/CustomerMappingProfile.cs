using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Student, StudentDto>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => String.Format("{0:#(###) ###-####}", src.PhoneNumber)));
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<AddNewStudentCommand, Student>()
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
            CreateMap<UpdateStudentCommand, Student>()
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
        }
    }
}
