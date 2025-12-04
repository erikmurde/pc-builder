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
using Public.DTO.V1.ComponentAttributes;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing component attributes
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
    public class ComponentAttributesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentAttributeMapper _mapper;

        /// <summary>
        /// Constructor for component attributes controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public ComponentAttributesController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ComponentAttributeMapper(mapper);
        }

        /// <summary>
        /// Get list of component attributes.
        /// </summary>
        /// <returns>List of ComponentAttributeDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ComponentAttributeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ComponentAttributeDTO>>> GetComponentAttributes()
        {
            var componentAttributes = 
                await _bll.ComponentAttributeService.AllAsync();

            return componentAttributes
                .Select(c => _mapper.Map(c))
                .ToList();
        }

        /// <summary>
        /// Get component attribute by id.
        /// </summary>
        /// <param name="id">Id of component attribute to get</param>
        /// <returns>ComponentAttributeDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentAttributeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComponentAttributeDTO>> GetComponentAttribute(Guid id)
        {
            var componentAttribute = await _bll.ComponentAttributeService.FindAsync(id);
            if (componentAttribute == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(componentAttribute);
        }
        
        /// <summary>
        /// Get component attribute by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of component attribute to get</param>
        /// <returns>ComponentAttributeEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentAttributeEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComponentAttributeEditDTO>> GetComponentAttributeEdit(Guid id)
        {
            var componentAttribute = await _bll.ComponentAttributeService.FindAsyncEdit(id);
            if (componentAttribute == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapEdit(componentAttribute);
        }

        /// <summary>
        /// Edit component attribute by id. Cannot edit componentId
        /// </summary>
        /// <param name="id">Id of component attribute to edit</param>
        /// <param name="componentAttribute">ComponentAttributeEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutComponentAttribute(Guid id, ComponentAttributeEditDTO componentAttribute)
        {
            if (id != componentAttribute.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.ComponentAttributeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            var bllComponentAttribute = _mapper.MapEdit(componentAttribute);

            try
            {
                await _bll.ComponentAttributeService.Update(bllComponentAttribute);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message == "Duplicate" 
                    ? EntityErrorHelper.DuplicateAttributeError() 
                    : EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new component attribute
        /// </summary>
        /// <param name="componentAttribute">
        /// ComponentAttributeCreateDTO with values of new component attribute</param>
        /// <returns>ComponentAttributeCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentAttributeCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ComponentAttributeCreateDTO>> PostComponentAttribute(
            ComponentAttributeCreateDTO componentAttribute)
        {
            var bllComponentAttribute = _mapper.MapCreate(componentAttribute);
            
            try
            {
                bllComponentAttribute = await _bll.ComponentAttributeService.Add(bllComponentAttribute);
                await _bll.SaveChangesAsync(); 
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message == "Duplicate" 
                    ? EntityErrorHelper.DuplicateAttributeError()
                    : EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetComponentAttribute", new { id = bllComponentAttribute.Id }, 
                bllComponentAttribute);
        }

        /// <summary>
        /// Delete component attribute by id
        /// </summary>
        /// <param name="id">Id of component attribute to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComponentAttribute(Guid id)
        {
            if (await _bll.ComponentAttributeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.ComponentAttributeService.RemoveAsync(id);
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