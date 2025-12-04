using System.Net.Mime;
using Asp.Versioning;
using BLL.Contracts.App;
using DAL.EF.Base;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.V1;
using Public.DTO.V1.Order;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing user orders
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _mapper;

        /// <summary>
        /// Constructor for orders controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public OrdersController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new OrderMapper(mapper);
        }

        /// <summary>
        /// Get list of orders. If user is not admin, get list of orders belonging to user.
        /// </summary>
        /// <returns>List of OrderDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = User.IsInRole(UserRole.Admin)
                ? await _bll.OrderService.AllAsync()
                : await _bll.OrderService.AllAsync(User.GetUserId());
            
            return orders
                .Select(o => _mapper.Map(o))
                .ToList();
        }

        /// <summary>
        /// Get order by id. If user is not admin, get order belonging to user
        /// </summary>
        /// <param name="id">Id of order to get</param>
        /// <returns>OrderDetailsDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrder(Guid id)
        {
            var order = User.IsInRole(UserRole.Admin)
                ? await _bll.OrderService.FindAsyncDetails(id)
                : await _bll.OrderService.FindAsyncDetails(id, User.GetUserId());

            if (order == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapDetails(order);
        }
        
        /// <summary>
        /// Get order by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of order to get</param>
        /// <returns>OrderEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<OrderEditDTO>> GetOrderEdit(Guid id)
        {
            var order = await _bll.OrderService.FindAsyncEdit(id);
            if (order == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapEdit(order);
        }

        /// <summary>
        /// Edit order by id. Only comment and status can be edited.
        /// Regular users can only cancel orders.
        /// If user is not admin then order must belong to user.
        /// </summary>
        /// <param name="id">Id of order to edit</param>
        /// <param name="order">OrderEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutOrder(Guid id, OrderEditDTO order)
        {
            if (id != order.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }

            var userIsAdmin = User.IsInRole(UserRole.Admin);

            if (userIsAdmin 
                    ? await _bll.OrderService.FindAsyncEdit(id) == null 
                    : await _bll.OrderService.FindAsyncEdit(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.OrderService.Update(_mapper.MapEdit(order), userIsAdmin);
                await _bll.SaveChangesAsync();
            } catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order">OrderCreateDTO that contains order details, list of OrderPcs,
        /// list of OrderShippingCosts and list of Payments</param>
        /// <returns>OrderCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderCreateDTO>> PostOrder(OrderCreateDTO order)
        {
            var bllOrder = _mapper.MapCreate(order);

            try
            {
                bllOrder = await _bll.OrderService.Add(bllOrder, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetOrder", new { id = bllOrder.Id }, bllOrder);
        }
    }
}