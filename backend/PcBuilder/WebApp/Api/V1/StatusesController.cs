using System.Net.Mime;
using Asp.Versioning;
using BLL.Contracts.App;
using DAL.EF.Base;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.V1;
using Public.DTO.V1.Status;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing order statuses
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly StatusMapper _mapper;

        /// <summary>
        /// Constructor for order statuses controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public StatusesController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new StatusMapper(mapper);
        }

        /// <summary>
        /// Get list of order statuses
        /// </summary>
        /// <returns>List of StatusDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<StatusDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatuses()
        {
            var statuses = await _bll.StatusService.AllAsync();

            return statuses
                .Select(s => _mapper.Map(s))
                .ToList();
        }

        /// <summary>
        /// Get order status by id
        /// </summary>
        /// <param name="id">ID of status to get</param>
        /// <returns>StatusDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<StatusDTO>> GetStatus(Guid id)
        {
            var status = await _bll.StatusService.FindAsync(id);
            if (status == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(status);
        }

        /// <summary>
        /// Edit order status by id
        /// </summary>
        /// <param name="id">Id of status to edit</param>
        /// <param name="status">StatusDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutStatus(Guid id, StatusDTO status)
        {
            if (id != status.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.StatusService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.StatusService.Update(_mapper.Map(status));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new order status
        /// </summary>
        /// <param name="status">StatusCreateDTO with values of new status</param>
        /// <returns>StatusCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StatusCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<StatusCreateDTO>> PostStatus(StatusCreateDTO status)
        {
            var bllStatus = _mapper.MapCreate(status);

            try
            {
                bllStatus = _bll.StatusService.Add(bllStatus);
                await _bll.SaveChangesAsync();
            } catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetStatus", new { id = bllStatus.Id }, bllStatus);
        }

        /// <summary>
        /// Delete order status by id
        /// </summary>
        /// <param name="id">Id of status to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            if (await _bll.StatusService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.StatusService.RemoveAsync(id);
                await _bll.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(EntityErrorHelper.CannotDeleteEntityError());
            }
        }
    }
}