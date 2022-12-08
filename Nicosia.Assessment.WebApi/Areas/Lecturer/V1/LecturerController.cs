using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;

namespace Nicosia.Assessment.WebApi.Areas.Lecturer.V1
{
    [Route("api/lecturer/v1/[controller]")]
    [NicosiaAuthorize("lecturer")]
    public class LecturerController : BaseController
    {
        private readonly IMediator _mediator;

        public LecturerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// List Of Students 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Lecturers list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<StudentForLecturerDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("student-list")]
        public async Task<IActionResult> GetList([FromQuery] GetStudentListForLecturerQuery getStudentListQuery, CancellationToken cancellationToken)
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
    }
}
