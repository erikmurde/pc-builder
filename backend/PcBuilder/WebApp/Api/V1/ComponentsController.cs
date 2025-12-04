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
using Public.DTO.V1.Component;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing components
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentMapper _mapper;

        /// <summary>
        /// Constructor for components controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public ComponentsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ComponentMapper(mapper);
        }

        /// <summary>
        /// Get list of components
        /// </summary>
        /// <returns>List of ComponentDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ComponentDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ComponentDTO>>> GetComponents()
        {
            var components = await _bll.ComponentService.AllAsync();

            return components
                .Select(c => _mapper.Map(c))
                .ToList();
        }
        
        /// <summary>
        /// Get list of simple components that only contain id, category name and component name.
        /// Used for filling client-side select elements with values without having to return a full DTO.
        /// </summary>
        /// <returns>List of ComponentSimpleDTOs</returns>
        [HttpGet("simple")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ComponentSimpleDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ComponentSimpleDTO>>> GetComponentsSimple()
        {
            var components = await _bll.ComponentService.AllAsyncSimple();

            return components
                .Select(c => _mapper.MapSimple(c))
                .ToList();
        }
        
        /// <summary>
        /// Get list of motherboards with component attributes.
        /// Used to filter out invalid motherboards in configurator.
        /// </summary>
        /// <returns>List of ComponentDetailsDTOs with category "Motherboard"</returns>
        [HttpGet("motherboard")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ComponentDetailsDTO>), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<ComponentDetailsDTO>>> GetMotherboards()
        {
            var components = await _bll.ComponentService.AllAsyncMotherboard();

            return components
                .Select(c => _mapper.MapDetails(c))
                .ToList();
        }
        
        /// <summary>
        /// Get list of detailed components whose ids match given values.
        /// Used to fetch initially selected components in configurator.
        /// </summary>
        /// <returns>List of ComponentDetailsDTOs with category "Motherboard"</returns>
        [HttpGet("selected/{pcBuildId:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ComponentDetailsDTO>), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<ComponentDetailsDTO>>> GetSelected(Guid pcBuildId)
        {
            var values = await _bll.PcBuildService.FindAsyncEdit(pcBuildId);
            if (values == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            var components = await _bll.ComponentService
                .AllAsyncSelected(values);

            return components
                .Select(c => _mapper.MapDetails(c))
                .ToList();
        }

        /// <summary>
        /// Get component by id
        /// </summary>
        /// <param name="id">Id of component to get</param>
        /// <returns>ComponentDetailsDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComponentDetailsDTO>> GetComponent(Guid id)
        {
            var component = await _bll.ComponentService.FindAsyncDetails(id);
            if (component == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapDetails(component);
        }
        
        /// <summary>
        /// Get component by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of component to get</param>
        /// <returns>ComponentEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComponentEditDTO>> GetComponentEdit(Guid id)
        {
            var component = await _bll.ComponentService.FindAsyncEdit(id);
            if (component == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapEdit(component);
        }

        /// <summary>
        /// Edit component by id
        /// </summary>
        /// <param name="id">Id of component to edit</param>
        /// <param name="component">ComponentEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutComponent(Guid id, ComponentEditDTO component)
        {
            if (id != component.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.ComponentService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            var bllComponent = _mapper.MapEdit(component);

            try
            {
                _bll.ComponentService.Update(bllComponent);
                await _bll.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new component
        /// </summary>
        /// <param name="component">ComponentCreateDTO with values of new component</param>
        /// <returns>ComponentCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComponentCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<ComponentCreateDTO>> PostComponent(ComponentCreateDTO component)
        {
            var bllComponent = _mapper.MapCreate(component);

            try
            {
                bllComponent = _bll.ComponentService.Add(bllComponent);
                await _bll.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetComponent", new { id = bllComponent.Id }, bllComponent);
        }

        /// <summary>
        /// Delete component by id. Deletes all attributes belonging to component
        /// </summary>
        /// <param name="id">Id of component to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteComponent(Guid id)
        {
            try
            {
                if (await _bll.ComponentService.RemoveAsync(id) == null)
                {
                    return NotFound(EntityErrorHelper.CannotFetchEntityError());
                }
                
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