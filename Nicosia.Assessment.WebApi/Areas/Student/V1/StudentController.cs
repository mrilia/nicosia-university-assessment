using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Queries;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;

namespace Nicosia.Assessment.WebApi.Areas.Student.V1
{
    [Route("api/student/v1/[controller]")]
    [NicosiaAuthorize("student")]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> GetClassList([FromQuery] GetClassListForStudentQuery getClassListQuery, CancellationToken cancellationToken)
        {
            var currentStudent= HttpContext.Items["User"]! as StudentDto;

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
