using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.ApproveMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.RejectMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class ApprovalRequestMappingProfile : Profile
    {
        private readonly IApprovalRequestContext _approvalRequestContext;

        public ApprovalRequestMappingProfile(IApprovalRequestContext approvalRequestContext)
        {
            _approvalRequestContext = approvalRequestContext;

            CreateMap<ApprovalRequest, ApprovalRequestDto>();
            CreateMap<AddNewMessagingRequestCommand, ApprovalRequest>();
            CreateMap<AddNewMessagingRequest, AddNewMessagingRequestCommand>()
                .ForMember(x => x.StudentId, opt => opt.MapFrom(src => src!.GetStudentId()));
            
            CreateMap<ApprovalRequest, ApproveRequestDto>();
            CreateMap<ApproveMessagingRequest, ApproveMessagingRequestCommand>()
                .ForMember(x => x.LecturerId, opt => opt.MapFrom(src => src!.GetLecturerId()))
                .ForMember(x => x.SectionId,
                    opt => opt.MapFrom(src =>
                        approvalRequestContext.ApprovalRequests!.Find(src.ApprovalRequestId)!.SectionId));
        
            CreateMap<ApprovalRequest, RejectRequestDto>();
            CreateMap<RejectMessagingRequest, RejectMessagingRequestCommand>()
                .ForMember(x => x.LecturerId, opt => opt.MapFrom(src => src!.GetLecturerId()))
                .ForMember(x => x.SectionId,
                    opt => opt.MapFrom(src =>
                        approvalRequestContext.ApprovalRequests!.Find(src.ApprovalRequestId)!.SectionId));

            CreateMap<ApprovalRequest, MessagingRequestDto>();

        }
    }
}
