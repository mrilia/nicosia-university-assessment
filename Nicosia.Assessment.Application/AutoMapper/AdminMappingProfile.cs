using AutoMapper;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.AddNewAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.UpdateAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Domain.Models.Security;
using Nicosia.Assessment.Domain.Models.User;
using Nicosia.Assessment.Shared.Encryption;
using Nicosia.Assessment.Shared.Token.JWT.Models;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class AdminMappingProfile : Profile
    {
        public AdminMappingProfile()
        {
            CreateMap<Admin, AdminDto>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => String.Format("{0:#(###) ###-####}", src.PhoneNumber)));
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            
            CreateMap<AdminDto, Admin>();

            CreateMap<AddNewAdminCommand, Admin>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.Password) ));
          
            CreateMap<UpdateAdminCommand, Admin>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.Password)));
           
            CreateMap<JwtRefreshToken, RefreshToken>();

            CreateMap<AuthenticateResponse, AuthenticateAdminResponse>();
        }
    }
}
