using System.Net.Mime;
using Asp.Versioning;
using BLL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.V1;
using Public.DTO.V1.PackageSize;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing package sizes
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PackageSizesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PackageSizeMapper _mapper;

        /// <summary>
        /// Constructor for package sizes controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public PackageSizesController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new PackageSizeMapper(mapper);
        }

        /// <summary>
        /// Get list of package sizes
        /// </summary>
        /// <returns>List of PackageSizeDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<PackageSizeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PackageSizeDTO>>> GetPackageSizes()
        {
            var sizes = await _bll.PackageSizeService.AllAsync();

            return sizes
                .Select(p => _mapper.Map(p))
                .ToList();
        }

        /// <summary>
        /// Get package size by id
        /// </summary>
        /// <param name="id">Id of package size to get</param>
        /// <returns>PackageSizeDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PackageSizeDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PackageSizeDTO>> GetPackageSize(Guid id)
        {
            var size = await _bll.PackageSizeService.FindAsync(id);
            if (size == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(size);
        }

        /// <summary>
        /// Edit package size by id
        /// </summary>
        /// <param name="id">Id of package size to edit</param>
        /// <param name="packageSize">PackageSizeDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutPackageSize(Guid id, PackageSizeDTO packageSize)
        {
            if (id != packageSize.Id)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }
            if (await _bll.PackageSizeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.PackageSizeService.Update(_mapper.Map(packageSize));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new package size
        /// </summary>
        /// <param name="packageSize">PackageSizeCreateDTO with values of new package size</param>
        /// <returns>PackageSizeDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PackageSizeCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<PackageSize>> PostPackageSize(PackageSizeCreateDTO packageSize)
        {
            var bllPackageSize = _mapper.MapCreate(packageSize);

            try
            {
                bllPackageSize = _bll.PackageSizeService.Add(bllPackageSize);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetPackageSize", new { id = bllPackageSize.Id }, bllPackageSize);
        }

        /// <summary>
        /// Delete package size by id
        /// </summary>
        /// <param name="id">Id of package size to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeletePackageSize(Guid id)
        {
            if (await _bll.PackageSizeService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.PackageSizeService.RemoveAsync(id);
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