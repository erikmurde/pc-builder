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
using Public.DTO.V1.CartPc;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing cart PCs
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartPcsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartPcMapper _mapper;

        /// <summary>
        /// Constructor for cart PCs controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public CartPcsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new CartPcMapper(mapper);
        }

        /// <summary>
        /// Get list of cart PCs. Cart PCs must belong to the user
        /// </summary>
        /// <returns>List of CartPcDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<CartPcDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CartPcDTO>>> GetCartPcs()
        {
            var cartPcs = await _bll.CartPcService.AllAsync(User.GetUserId());

            return cartPcs
                .Select(c => _mapper.Map(c))
                .ToList();
        }

        /// <summary>
        /// Get cart PC by id. Cart PC must belong to the user
        /// </summary>
        /// <param name="id">Id of cart PC to get</param>
        /// <returns>CartPcDetailsDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CartPcDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartPcDTO>> GetCartPc(Guid id)
        {
            var cartPc = await _bll.CartPcService.FindAsync(id, User.GetUserId());
            if (cartPc == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            return _mapper.Map(cartPc);
        }

        /// <summary>
        /// Edit cart PC by id. Cart PC must belong to the user
        /// </summary>
        /// <param name="id">Id of cart PC to edit</param>
        /// <param name="cartPc">CartPcEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCartPc(Guid id, CartPcEditDTO cartPc)
        {
            if (id != cartPc.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.CartPcService.FindAsync(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            var bllCartPc = _mapper.MapEdit(cartPc);

            try
            {
                await _bll.CartPcService.Update(bllCartPc);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new cart PC
        /// </summary>
        /// <param name="cartPc">CartPcCreateDTO with values of new cart PC</param>
        /// <returns>CartPcCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CartPcCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartPcCreateDTO>> PostCartPc(CartPcCreateDTO cartPc)
        {
            var bllCartPc = _mapper.MapCreate(cartPc);

            try
            {
                bllCartPc = await _bll.CartPcService.Add(bllCartPc, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetCartPc", new { id = bllCartPc.Id }, bllCartPc);
        }

        /// <summary>
        /// Delete cart PC by id. Cart PC must belong to the user
        /// </summary>
        /// <param name="id">Id of cart PC to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCartPc(Guid id)
        {
            if (await _bll.CartPcService.FindAsync(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.CartPcService.RemoveAsync(id);
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