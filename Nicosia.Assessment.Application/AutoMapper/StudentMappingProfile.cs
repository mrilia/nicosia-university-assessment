﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Domain.Models.User;
using Nicosia.Assessment.Shared.Encryption;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, StudentDto>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => String.Format("{0:#(###) ###-####}", src.PhoneNumber)));
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<AddNewStudentCommand, Student>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.PhoneNumber) ));
            CreateMap<UpdateStudentCommand, Student>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Hash(src.PhoneNumber)));
        }
    }
}
