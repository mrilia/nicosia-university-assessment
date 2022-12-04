﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Course.Commands.AddNewCourse;
using Nicosia.Assessment.Application.Handlers.Course.Commands.DeleteCourse;
using Nicosia.Assessment.Application.Handlers.Course.Commands.UpdateCourse;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Application.Handlers.Course.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Course.V1
{
    public class CourseController : BaseController
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new Course
        /// </summary>
        /// <param name="addNewCourseCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Course successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CourseDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewCourseCommand addNewCourseCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewCourseCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetCourseInfo", new { id = result.Data.CourseId }), result.Data);
        }



        /// <summary>
        /// Course Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Course info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If Course not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CourseDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetCourseInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCourseQuery { CourseId = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Courses 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Courses list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<CourseDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string code, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetCourseListQuery { Code = code }, cancellationToken));




        /// <summary>
        /// Delete Course
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If Course not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCourseCommand { CourseId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Course
        /// </summary>
        /// <param name="updateCourseCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update Course successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseCommand updateCourseCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateCourseCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
