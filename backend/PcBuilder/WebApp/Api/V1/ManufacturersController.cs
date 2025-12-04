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
using Public.DTO.V1.Manufacturer;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing manufacturers
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ManufacturerMapper _mapper;

        /// <summary>
        /// Constructor for manufacturers controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public ManufacturersController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ManufacturerMapper(mapper);
        }

        /// <summary>
        /// Get list of manufacturers
        /// </summary>
        /// <returns>List of ManufacturerDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ManufacturerDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ManufacturerDTO>>> GetManufacturers()
        {
            var manufacturers = await _bll.ManufacturerService.AllAsync();

            return manufacturers
                .Select(m => _mapper.Map(m))
                .ToList();
        }

        /// <summary>
        /// Get manufacturer by id
        /// </summary>
        /// <param name="id">Id of manufacturer to get</param>
        /// <returns>ManufacturerDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ManufacturerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ManufacturerDTO>> GetManufacturer(Guid id)
        {
            var manufacturer = await _bll.ManufacturerService.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(manufacturer);
        }

        /// <summary>
        /// Edit manufacturer by id
        /// </summary>
        /// <param name="id">Id of manufacturer to edit</param>
        /// <param name="manufacturer">ManufacturerDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutManufacturer(Guid id, ManufacturerDTO manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.ManufacturerService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.ManufacturerService.Update(_mapper.Map(manufacturer));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new manufacturer
        /// </summary>
        /// <param name="manufacturer">ManufacturerCreateDTO with values of new manufacturer</param>
        /// <returns>ManufacturerCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ManufacturerCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<ManufacturerCreateDTO>> PostManufacturer(ManufacturerCreateDTO manufacturer)
        {
            var bllManufacturer = _mapper.MapCreate(manufacturer);

            try
            {
                bllManufacturer = _bll.ManufacturerService.Add(bllManufacturer);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetManufacturer", new { id = bllManufacturer.Id }, bllManufacturer);
        }

        /// <summary>
        /// Delete manufacturer by id
        /// </summary>
        /// <param name="id">Id of manufacturer to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            if (await _bll.ManufacturerService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.ManufacturerService.RemoveAsync(id);
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