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
using Public.DTO.V1.Attribute;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing attributes
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AttributesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AttributeMapper _mapper;

        /// <summary>
        /// Constructor for attributes controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public AttributesController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new AttributeMapper(mapper);
        }

        /// <summary>
        /// Get list of attributes
        /// </summary>
        /// <returns>List of AttributeDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<AttributeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AttributeDTO>>> GetAttributes()
        {
            var attributes = await _bll.AttributeService.AllAsync();

            return attributes
                .Select(a => _mapper.Map(a))
                .ToList();
        }

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id">Id of attribute to get</param>
        /// <returns>AttributeDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AttributeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AttributeDTO>> GetAttribute(Guid id)
        {
            var attribute = await _bll.AttributeService.FindAsync(id);
            if (attribute == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(attribute);
        }

        /// <summary>
        /// Edit attribute by id
        /// </summary>
        /// <param name="id">Id of attribute to edit</param>
        /// <param name="attribute">AttributeDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutAttribute(Guid id, AttributeDTO attribute)
        {
            if (id != attribute.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.AttributeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.AttributeService.Update(_mapper.Map(attribute));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new attribute
        /// </summary>
        /// <param name="attribute">AttributeCreateDTO with values of new attribute</param>
        /// <returns>AttributeCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AttributeCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<AttributeCreateDTO>> PostAttribute(AttributeCreateDTO attribute)
        {
            var bllAttribute = _mapper.MapCreate(attribute);

            try
            {
                bllAttribute = _bll.AttributeService.Add(bllAttribute);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetAttribute", new { id = bllAttribute.Id }, bllAttribute);
        }

        /// <summary>
        /// Delete attribute by id
        /// </summary>
        /// <param name="id">Id of attribute to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteAttribute(Guid id)
        {
            if (await _bll.AttributeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.AttributeService.RemoveAsync(id);
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