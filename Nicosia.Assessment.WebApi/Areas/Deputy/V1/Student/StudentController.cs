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
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.Deputy.V1.Student
{
    [Route("api/deputy/v1/[controller]")]
    [NicosiaAuthorize("admin")]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Add new student
        /// Sample Phone Number: +989123123456
        /// </summary>
        /// <param name="addNewStudentCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create student successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(StudentDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] { "Deputy: Students Operations" })]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewStudentCommand addNewStudentCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewStudentCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetStudentInfo", new { id = result.Data.StudentId }), result.Data);
        }



        /// <summary>
        /// Student Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Student info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If student not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(StudentDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] { "Deputy: Students Operations" })]
        [HttpGet("{id}", Name = "GetStudentInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery { StudentId = id }, cancellationToken);

            return result.ApiResult;
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
        [SwaggerOperation(Tags = new[] { "Deputy: Students Operations" })]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(GetStudentListQuery getStudentListQuery, CancellationToken cancellationToken)
        {
            var students = await _mediator.Send(getStudentListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getStudentListQuery, students.Count);
            var result = new PaginationResponse<StudentDto>(students, students.Count, nextPageUrl);

            return Ok(result);
        }



        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If student not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] { "Deputy: Students Operations" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { StudentId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Student
        /// </summary>
        /// <param name="updateStudentCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update student successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Students Operations" })]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand updateStudentCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateStudentCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
