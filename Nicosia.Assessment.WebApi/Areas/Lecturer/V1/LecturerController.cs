using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.ApproveMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.RejectMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Lecturer.Queries;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Queries;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.Lecturer.V1
{
    [Route("api/lecturer/v1/[controller]")]
    [NicosiaAuthorize("lecturer")]
    public class LecturerController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public LecturerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        /// <summary>
        /// List Of Students 
        /// </summary>
        /// <param name="getStudentListQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Lecturers list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<StudentForLecturerDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("student-list")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> GetStudentList([FromQuery] GetStudentListForLecturerQuery getStudentListQuery, CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer == null)
                throw new AuthenticationException("No claim found!");

            getStudentListQuery.SetLecturerId(currentLecturer.LecturerId);

            var students = await _mediator.Send(getStudentListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getStudentListQuery, students.TotalCount);
            var result = new PaginationResponse<StudentForLecturerDto>(students.Items, students.TotalCount, nextPageUrl);

            return Ok(result);
        }


        /// <summary>
        /// List Of Classes 
        /// </summary>
        /// <param name="getClassListQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Class list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<ClassReportDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("class-list")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> GetClassList([FromQuery] GetClassListForLecturerQuery getClassListQuery, CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer == null)
                throw new AuthenticationException("No claim found!");

            getClassListQuery.SetLecturerId(currentLecturer.LecturerId);

            var students = await _mediator.Send(getClassListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getClassListQuery, students.TotalCount);
            var result = new PaginationResponse<ClassReportDto>(students.Items, students.TotalCount, nextPageUrl);

            return Ok(result);
        }

        /// <summary>
        /// Approve Messaging Request
        /// </summary>
        /// <param name="approveMessagingRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Messaging Request successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApproveRequestDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("approve-request")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> AddNew(ApproveMessagingRequest approveMessagingRequest,
            CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer == null)
                throw new AuthenticationException("No claim found!");

            approveMessagingRequest.SetLecturerId(currentLecturer.LecturerId);

            var approveMessagingRequestCommand = _mapper.Map<ApproveMessagingRequestCommand>(approveMessagingRequest);

            var result = await _mediator.Send(approveMessagingRequestCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }

        
        /// <summary>
        /// Reject Messaging Request
        /// </summary>
        /// <param name="rejectMessagingRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Messaging Request successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(RejectRequestDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("reject-request")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> AddNew(RejectMessagingRequest rejectMessagingRequest,
            CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer == null)
                throw new AuthenticationException("No claim found!");

            rejectMessagingRequest.SetLecturerId(currentLecturer.LecturerId);

            var rejectMessagingRequestCommand = _mapper.Map<RejectMessagingRequestCommand>(rejectMessagingRequest);

            var result = await _mediator.Send(rejectMessagingRequestCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        /// <summary>
        /// List Of Messaging Requests 
        /// </summary>
        /// <param name="getMessagingRequestListQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Messaging Requests list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<MessagingRequestDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("messaging-request-list")]
        [SwaggerOperation(Tags = new[] { "Major Assessment Endpoints" })]
        public async Task<IActionResult> GetMessagingRequestList([FromQuery] GetMessagingRequestListQuery getMessagingRequestListQuery, CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer == null)
                throw new AuthenticationException("No claim found!");

            getMessagingRequestListQuery.SetLecturerId(currentLecturer.LecturerId);

            var requests = await _mediator.Send(getMessagingRequestListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getMessagingRequestListQuery, requests.TotalCount);
            var result = new PaginationResponse<MessagingRequestDto>(requests.Items, requests.TotalCount, nextPageUrl);

            return Ok(result);
        }


    }
}
