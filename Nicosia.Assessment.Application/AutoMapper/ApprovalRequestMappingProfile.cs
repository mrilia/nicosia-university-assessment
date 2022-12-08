using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class ApprovalRequestMappingProfile : Profile
    {
        public ApprovalRequestMappingProfile()
        {
            CreateMap<ApprovalRequest, ApprovalRequestDto>();
            CreateMap<AddNewMessagingRequestCommand, ApprovalRequest>();
            CreateMap<AddNewMessagingRequest, AddNewMessagingRequestCommand>()
                .ForMember(x => x.StudentId, opt => opt.MapFrom(src => src!.GetStudentId()));
        }
    }
}
