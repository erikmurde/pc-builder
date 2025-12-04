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
using Public.DTO.V1.Discount;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing discounts
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DiscountMapper _mapper;

        /// <summary>
        /// Constructor for discounts controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public DiscountsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new DiscountMapper(mapper);
        }

        /// <summary>
        /// Get list of discounts
        /// </summary>
        /// <returns>List of DiscountDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<DiscountDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetDiscounts()
        {
            var discounts = await _bll.DiscountService.AllAsync();

            return discounts
                .Select(d => _mapper.Map(d))
                .ToList();
        }

        /// <summary>
        /// Get discount by id
        /// </summary>
        /// <param name="id">Id of discount to get</param>
        /// <returns>DiscountDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DiscountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountDTO>> GetDiscount(Guid id)
        {
            var discount = await _bll.DiscountService.FindAsync(id);
            if (discount == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(discount);
        }

        /// <summary>
        /// Edit discount by id
        /// </summary>
        /// <param name="id">Id of discount to edit</param>
        /// <param name="discount">DiscountDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutDiscount(Guid id, DiscountDTO discount)
        {
            if (id != discount.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.DiscountService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.DiscountService.Update(_mapper.Map(discount));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new discount
        /// </summary>
        /// <param name="discount">DiscountCreateDTO with values of new discount</param>
        /// <returns>DiscountCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DiscountCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<DiscountCreateDTO>> PostDiscount(DiscountCreateDTO discount)
        {
            var bllDiscount = _mapper.MapCreate(discount);

            try
            {
                bllDiscount = _bll.DiscountService.Add(bllDiscount);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetDiscount", new { id = bllDiscount.Id }, bllDiscount);
        }

        /// <summary>
        /// Delete discount by id
        /// </summary>
        /// <param name="id">Id of discount to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            if (await _bll.DiscountService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.DiscountService.RemoveAsync(id);
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