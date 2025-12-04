using System.Net.Mime;
using Asp.Versioning;
using BLL.Contracts.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.V1;
using Public.DTO.V1.PcBuild;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing PC builds
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PcBuildsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PcBuildMapper _mapper;

        /// <summary>
        /// Constructor for PC builds controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public PcBuildsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new PcBuildMapper(mapper);
        }

        /// <summary>
        /// Get list of PC builds
        /// </summary>
        /// <returns>List of PcBuildDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<PcBuildDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PcBuildDTO>>> GetPcBuilds()
        {
            var pcBuilds = await _bll.PcBuildService.AllAsync();

            return pcBuilds
                .Select(p => _mapper.Map(p))
                .ToList();
        }
        
        /// <summary>
        /// Get list of PC builds with component names. Used in store for filtering
        /// </summary>
        /// <returns>List of PcBuildStoreDTOs</returns>
        [HttpGet("store")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<PcBuildStoreDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PcBuildStoreDTO>>> GetPcBuildsStore()
        {
            var pcBuilds = await _bll.PcBuildService.AllAsyncStore();

            return pcBuilds
                .Select(p => _mapper.MapStore(p))
                .ToList();
        }

        /// <summary>
        /// Get PC build by id
        /// </summary>
        /// <param name="id">Id of PC build to get</param>
        /// <returns>PcBuildDetailsDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PcBuildDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PcBuildDetailsDTO>> GetPcBuild(Guid id)
        {
            var bllPcBuild = await _bll.PcBuildService.FindAsyncDetails(id);
            if (bllPcBuild == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapDetails(bllPcBuild);
        }
        
        /// <summary>
        /// Get PC build by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of PC build to get</param>
        /// <returns>PcBuildEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PcBuildEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PcBuildEditDTO>> GetPcBuildEdit(Guid id)
        {
            var bllPcBuild = await _bll.PcBuildService.FindAsyncEdit(id);
            if (bllPcBuild == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            return _mapper.MapEdit(bllPcBuild);
        }

        /// <summary>
        /// Edit PC build by id
        /// </summary>
        /// <param name="id">Id of PC build to edit</param>
        /// <param name="pcBuild">PcBuildEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutPcBuild(Guid id, PcBuildEditDTO pcBuild)
        {
            if (id != pcBuild.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            } 
            if (await _bll.PcBuildService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            var bllPcBuild = _mapper.MapEdit(pcBuild);

            try
            { 
                await _bll.PcBuildService.Update(bllPcBuild);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"ERROR: {e.Message}", ConsoleColor.Red);
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new PC build with components
        /// </summary>
        /// <param name="pcBuild">PcBuildCreateDTO with values of new PC build</param>
        /// <returns>PcBuildCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PcBuildEditDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PcBuildEditDTO>> PostPcBuild(PcBuildCreateDTO pcBuild)
        {
            var bllPcBuild = _mapper.MapCreate(pcBuild);
            
            try
            {
                bllPcBuild = await _bll.PcBuildService.Add(bllPcBuild);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"ERROR: {e.Message}", ConsoleColor.Red);
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetPcBuild", new { id = bllPcBuild.Id }, bllPcBuild);
        }

        /// <summary>
        /// Delete PC build by id. Deletes all PcComponents belonging to PC build
        /// </summary>
        /// <param name="id">Id of PC build to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePcBuild(Guid id)
        {
            try
            {
                if (await _bll.PcBuildService.RemoveAsync(id) == null)
                {
                    return NotFound(EntityErrorHelper.CannotFetchEntityError());
                }
                return NoContent();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine($"ERROR: {e.Message}", ConsoleColor.Red);
                return BadRequest(EntityErrorHelper.CannotDeleteEntityError());
            }
        }
    }
}