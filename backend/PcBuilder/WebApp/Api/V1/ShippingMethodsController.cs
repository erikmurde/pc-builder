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
using Public.DTO.V1.ShippingMethod;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing shipping methods
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShippingMethodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ShippingMethodMapper _mapper;

        /// <summary>
        /// Constructor for shipping methods controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public ShippingMethodsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ShippingMethodMapper(mapper);
        }

        /// <summary>
        /// Get list of shipping methods
        /// </summary>
        /// <returns>List of ShippingMethodDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ShippingMethodDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShippingMethodDTO>>> GetShippingMethods()
        {
            var methods = await _bll.ShippingMethodService.AllAsync();

            return methods
                .Select(m => _mapper.Map(m))
                .ToList();
        }

        /// <summary>
        /// Get shipping method by id
        /// </summary>
        /// <param name="id">Id of shipping method to get</param>
        /// <returns>ShippingMethodDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShippingMethodDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingMethodDTO>> GetShippingMethod(Guid id)
        {
            var shippingMethod = await _bll.ShippingMethodService.FindAsync(id);
            if (shippingMethod == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(shippingMethod);
        }

        /// <summary>
        /// Edit shipping method by id
        /// </summary>
        /// <param name="id">Id of shipping method to edit</param>
        /// <param name="shippingMethod">ShippingMethodDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutShippingMethod(Guid id, ShippingMethodDTO shippingMethod)
        {
            if (id != shippingMethod.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.ShippingMethodService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.ShippingMethodService.Update(_mapper.Map(shippingMethod));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new shipping method
        /// </summary>
        /// <param name="shippingMethod">ShippingMethodCreateDTO with values of new shipping method</param>
        /// <returns>ShippingMethodCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShippingMethodCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<ShippingMethodCreateDTO>> PostShippingMethod(ShippingMethodCreateDTO shippingMethod)
        {
            var bllShippingMethod = _mapper.MapCreate(shippingMethod);

            try
            {
                bllShippingMethod = _bll.ShippingMethodService.Add(bllShippingMethod);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetShippingMethod", new { id = bllShippingMethod.Id }, 
                bllShippingMethod);
        }

        /// <summary>
        /// Delete shipping method by id
        /// </summary>
        /// <param name="id">Id of shipping method to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteShippingMethod(Guid id)
        {
            if (await _bll.ShippingMethodService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.ShippingMethodService.RemoveAsync(id);
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