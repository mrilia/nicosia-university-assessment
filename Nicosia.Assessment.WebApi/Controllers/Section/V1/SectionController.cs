using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Section.Commands.AddNewSection;
using Nicosia.Assessment.Application.Handlers.Section.Commands.DeleteSection;
using Nicosia.Assessment.Application.Handlers.Section.Commands.UpdateSection;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Section.V1
{
    public class SectionController : BaseController
    {
        private readonly IMediator _mediator;

        public SectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new Section
        /// </summary>
        /// <param name="addNewSectionCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Section successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SectionDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewSectionCommand addNewSectionCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewSectionCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetSectionInfo", new { id = result.Data.SectionId }), result.Data);
        }



        /// <summary>
        /// Section Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Section info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If Section not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SectionDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetSectionInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSectionQuery { SectionId = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Sections 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Sections list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<SectionDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string number, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetSectionListQuery { Number = number }, cancellationToken));




        /// <summary>
        /// Delete Section
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If Section not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteSectionCommand { SectionId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Section
        /// </summary>
        /// <param name="updateSectionCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update Section successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSectionCommand updateSectionCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateSectionCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
