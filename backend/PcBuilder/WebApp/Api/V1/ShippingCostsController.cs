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
using Public.DTO.V1.ShippingCost;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing shipping costs
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingCostsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ShippingCostMapper _mapper;

        /// <summary>
        /// Constructor for shipping costs controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public ShippingCostsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ShippingCostMapper(mapper);
        }

        /// <summary>
        /// Get list of shipping costs
        /// </summary>
        /// <returns>List of ShippingCostDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ShippingCostDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShippingCostDTO>>> GetShippingCosts()
        {
            var shippingCosts = await _bll.ShippingCostService.AllAsync();

            return shippingCosts
                .Select(s => _mapper.Map(s))
                .ToList();
        }

        /// <summary>
        /// Get shipping cost by id
        /// </summary>
        /// <param name="id">Id of shipping cost to get</param>
        /// <returns>ShippingCostDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShippingCostDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingCostDTO>> GetShippingCost(Guid id)
        {
            var shippingCost = await _bll.ShippingCostService.FindAsync(id);
            if (shippingCost == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(shippingCost);
        }
        
        /// <summary>
        /// Get shipping cost by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of shipping cost to get</param>
        /// <returns>ShippingCostEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShippingCostEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<ShippingCostEditDTO>> GetShippingCostEdit(Guid id)
        {
            var shippingCost = await _bll.ShippingCostService.FindAsyncEdit(id);
            if (shippingCost == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapEdit(shippingCost);
        }

        /// <summary>
        /// Edit shipping cost by id. Duplicates are not allowed
        /// </summary>
        /// <param name="id">Id of shipping cost to edit</param>
        /// <param name="shippingCost">ShippingCostEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutShippingCost(Guid id, ShippingCostEditDTO shippingCost)
        {
            if (id != shippingCost.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.ShippingCostService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.ShippingCostService.Update(_mapper.MapEdit(shippingCost));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new shipping cost. Duplicates are not allowed
        /// </summary>
        /// <param name="shippingCost">ShippingCostCreateDTO with values of new shipping cost</param>
        /// <returns>ShippingCostCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShippingCostCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<ShippingCostCreateDTO>> PostShippingCost(ShippingCostCreateDTO shippingCost)
        {
            var bllShippingCost = _mapper.MapCreate(shippingCost);

            try
            {
                bllShippingCost = await _bll.ShippingCostService.Add(bllShippingCost);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetShippingCost", new { id = bllShippingCost.Id }, bllShippingCost);
        }

        /// <summary>
        /// Delete shipping cost by id
        /// </summary>
        /// <param name="id">Id of shipping cost to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteShippingCost(Guid id)
        {
            if (await _bll.ShippingCostService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.ShippingCostService.RemoveAsync(id);
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