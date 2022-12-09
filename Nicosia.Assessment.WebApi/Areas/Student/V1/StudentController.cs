using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Queries;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters.AuthFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.Student.V1
{
    [Route("api/student/v1/[controller]")]
    [NicosiaAuthorize("student,admin")]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StudentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new Messaging Request
        /// </summary>
        /// <param name="addNewMessagingRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Messaging Request successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApprovalRequestDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("request-approval")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> AddNew(AddNewMessagingRequest addNewMessagingRequest,
            CancellationToken cancellationToken)
        {
            var currentStudent = HttpContext.Items["User"]! as StudentDto;

            if (currentStudent == null)
                throw new AuthenticationException("No claim found!");

            addNewMessagingRequest.SetStudentId(currentStudent.StudentId);

            var addNewMessagingRequestCommand = _mapper.Map<AddNewMessagingRequestCommand>(addNewMessagingRequest);

            var result = await _mediator.Send(addNewMessagingRequestCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        /// <summary>
        /// List Of Classmates
        /// </summary>
        /// <param name="getClassmateQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Classmate list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<ClassmateDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("classmate")]
        [SwaggerOperation(Tags = new[] {"Major Assessment Endpoints" })]
        public async Task<IActionResult> GetClassmateList([FromQuery] GetClassmateQuery getClassmateQuery, CancellationToken cancellationToken)
        {
            var currentStudent = HttpContext.Items["User"]! as StudentDto;

            if (currentStudent == null)
                throw new AuthenticationException("No claim found!");

            getClassmateQuery.SetStudentId(currentStudent.StudentId);

            var students = await _mediator.Send(getClassmateQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getClassmateQuery, students.TotalCount);
            var result = new PaginationResponse<ClassmateDto>(students.Items, students.TotalCount, nextPageUrl);

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
        public async Task<IActionResult> GetClassList([FromQuery] GetClassListForStudentQuery getClassListQuery, CancellationToken cancellationToken)
        {
            var currentStudent = HttpContext.Items["User"]! as StudentDto;

            if (currentStudent == null)
                throw new AuthenticationException("No claim found!");

            getClassListQuery.SetStudentId(currentStudent.StudentId);

            var students = await _mediator.Send(getClassListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getClassListQuery, students.TotalCount);
            var result = new PaginationResponse<ClassReportDto>(students.Items, students.TotalCount, nextPageUrl);

            return Ok(result);
        }
    }
}
