﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.AddNewLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.DeleteLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.UpdateLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Lecturer.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Lecturer.V1
{
    public class LecturerController : BaseController
    {
        private readonly IMediator _mediator;

        public LecturerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymousAttribute]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateLecturerCommand authenticateLecturerCommand, CancellationToken cancellationToken)
        {
            authenticateLecturerCommand.IpAdress = IpAddress();
            var response = _mediator.Send(authenticateLecturerCommand, cancellationToken).Result.Data;
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymousAttribute]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshLecturerTokenCommand refreshLecturerTokenCommand, CancellationToken cancellationToken)
        {
            refreshLecturerTokenCommand.IpAdress = IpAddress();
            refreshLecturerTokenCommand.RefreshToken =
                refreshLecturerTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            var response = _mediator.Send(refreshLecturerTokenCommand, cancellationToken).Result.Data;

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeLecturerTokenCommand revokeLecturerTokenCommand, CancellationToken cancellationToken)
        {
            // accept refresh token in request body or cookie
            var token = revokeLecturerTokenCommand.RefreshToken ?? Request.Cookies["refreshToken"];

            await _mediator.Send(revokeLecturerTokenCommand, cancellationToken);

            return Ok(new { message = "Token revoked" });
        }

        /// <summary>
        /// Add new lecturer
        /// Sample Phone Number: 044 668 18 00
        /// </summary>
        /// <param name="addNewLecturerCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create lecturer successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(LecturerDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewLecturerCommand addNewLecturerCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewLecturerCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetLecturerInfo", new { id = result.Data.LecturerId }), result.Data);
        }



        /// <summary>
        /// Lecturer Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Lecturer info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If lecturer not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(LecturerDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetLecturerInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLecturerByIdQuery { LecturerId = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Lecturers 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Lecturers list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<LecturerDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string email, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetLecturerListQuery { Email = email }, cancellationToken));




        /// <summary>
        /// Delete Lecturer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If lecturer not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteLecturerCommand { LecturerId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Lecturer
        /// </summary>
        /// <param name="updateLecturerCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update lecturer successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLecturerCommand updateLecturerCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateLecturerCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
