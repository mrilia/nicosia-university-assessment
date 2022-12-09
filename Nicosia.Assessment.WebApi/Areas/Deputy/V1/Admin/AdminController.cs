using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.AddNewAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.DeleteAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.UpdateAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Handlers.Admin.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters.AuthFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Nicosia.Assessment.WebApi.Areas.Deputy.V1.Admin
{
    [Route("api/deputy/v1/[controller]")]
    [NicosiaAuthorize("admin")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Add new admin
        /// </summary>
        /// <param name="addNewAdminCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create admin successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(AdminDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Admin Users Operations" })]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewAdminCommand addNewAdminCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewAdminCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetAdminInfo", new { id = result.Data.AdminId }), result.Data);
        }



        /// <summary>
        /// Admin Info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Admin info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If admin not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(AdminDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Admin Users Operations" })]
        [HttpGet("{id}", Name = "GetAdminInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAdminByIdQuery { AdminId = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Admins 
        /// </summary>
        /// <param name="getAdminListQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Admins list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<AdminDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Admin Users Operations" })]
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] GetAdminListQuery getAdminListQuery, CancellationToken cancellationToken)
        {
            var admins = await _mediator.Send(getAdminListQuery, cancellationToken);
            var nextPageUrl = GetNextPageUrl(getAdminListQuery, admins.TotalCount);
            var result = new PaginationResponse<AdminDto>(admins.Items, admins.TotalCount, nextPageUrl);

            return Ok(result);
        }



        /// <summary>
        /// Delete Admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If admin not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Admin Users Operations" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteAdminCommand { AdminId = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Admin
        /// </summary>
        /// <param name="updateAdminCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update admin successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [SwaggerOperation(Tags = new[] {"Deputy: Admin Users Operations" })]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAdminCommand updateAdminCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateAdminCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
