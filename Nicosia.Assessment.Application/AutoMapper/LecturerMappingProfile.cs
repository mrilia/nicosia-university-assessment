using AutoMapper;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.AddNewLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.UpdateLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Domain.Models.User;
using Nicosia.Assessment.Shared.Encryption;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class LecturerMappingProfile : Profile
    {
        public LecturerMappingProfile()
        {
            CreateMap<Lecturer, LecturerDto>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => String.Format("{0:#(###) ###-####}", src.PhoneNumber)));
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<AddNewLecturerCommand, Lecturer>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.Password) ));
            CreateMap<UpdateLecturerCommand, Lecturer>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.Password)));
        }
    }
}
