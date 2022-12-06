using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.DeleteStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;

namespace Nicosia.Assessment.WebApi.Areas.Student.V1
{
    [Route("api/student/v1/[controller]")]
    //[NicosiaAuthorize("student")]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// List Of Students 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Students list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<StudentDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("list")]
        public async Task<IActionResult> GetList(GetStudentListQuery getStudentListQuery, CancellationToken cancellationToken)
        {
            var students = await _mediator.Send(getStudentListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getStudentListQuery, students.Count);
            var result = new PaginationResponse<StudentDto>(students, students.Count, nextPageUrl);

            return Ok(result);
        }

        
    }
}
