using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.UpdateCustomer;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Domain.Models.Customer;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                 //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => String.Format("{0:#(###) ###-####}", src.PhoneNumber)));
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<AddNewCustomerCommand, Customer>()
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
            CreateMap<UpdateCustomerCommand, Customer>()
                 .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", ""))));
        }
    }
}
