using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Period.Commands.AddNewPeriod;
using Nicosia.Assessment.Application.Handlers.Period.Commands.DeletePeriod;
using Nicosia.Assessment.Application.Handlers.Period.Commands.UpdatePeriod;
using Nicosia.Assessment.Application.Handlers.Period.Dto;
using Nicosia.Assessment.Application.Handlers.Period.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.Deputy.V1.Period
{
    [Route("api/deputy/v1/[controller]")]
    [NicosiaAuthorize("admin")]
    public class PeriodController : BaseController
    {
        private readonly IMediator _mediator;

        public PeriodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new Period
        /// </summary>
        /// <param name="addNewPeriodCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create Period successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PeriodDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Periods Operations" })]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewPeriodCommand addNewPeriodCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewPeriodCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetPeriodInfo", new { id = result.Data.PeriodId }), result.Data);
        }



        /// <summary>
        /// Period Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Period info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If Period not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PeriodDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Periods Operations" })]
        [HttpGet("{id}", Name = "GetPeriodInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPeriodQuery { PeriodId = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Periods 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Periods list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<PeriodDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Periods Operations" })]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string name, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetPeriodListQuery { Name = name }, cancellationToken));




        /// <summary>
        /// Delete Period
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If Period not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Periods Operations" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePeriodCommand { PeriodId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Period
        /// </summary>
        /// <param name="updatePeriodCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update Period successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Periods Operations" })]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePeriodCommand updatePeriodCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updatePeriodCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
