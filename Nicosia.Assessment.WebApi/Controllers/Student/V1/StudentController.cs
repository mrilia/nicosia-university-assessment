using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.DeleteStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Student.V1
{
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateStudentCommand authenticateStudentCommand, CancellationToken cancellationToken)
        {
            authenticateStudentCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateStudentCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshStudentTokenCommand refreshStudentTokenCommand, CancellationToken cancellationToken)
        {
            refreshStudentTokenCommand.IpAdress = IpAddress();
            refreshStudentTokenCommand.RefreshToken =
                refreshStudentTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            var response = _mediator.Send(refreshStudentTokenCommand, cancellationToken).Result.Data;

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeStudentTokenCommand revokeStudentTokenCommand, CancellationToken cancellationToken)
        {
            // accept refresh token in request body or cookie
            var token = revokeStudentTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            await _mediator.Send(revokeStudentTokenCommand, cancellationToken);

            return Ok(new { message = "Token revoked" });
        }

        /// <summary>
        /// Add new student
        /// Sample Phone Number: 044 668 18 00
        /// </summary>
        /// <param name="addNewStudentCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create student successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(StudentDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
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
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string email, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetStudentListQuery { Email = email }, cancellationToken));




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
