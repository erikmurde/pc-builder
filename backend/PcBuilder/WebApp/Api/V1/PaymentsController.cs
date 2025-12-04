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
using Public.DTO.V1.Payment;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing user payments
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMapper _mapper;

        /// <summary>
        /// Constructor for payments controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public PaymentsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new PaymentMapper(mapper);
        }

        /// <summary>
        /// Get list of payments. If user is not admin, get list of payments belonging to user
        /// </summary>
        /// <returns>List of PaymentDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<PaymentDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            var payments = User.IsInRole(UserRole.Admin)
                ? await _bll.PaymentService.AllAsync()
                : await _bll.PaymentService.AllAsync(User.GetUserId());
            
            return payments
                .Select(p => _mapper.Map(p))
                .ToList();
        }

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id">Id of payment to get</param>
        /// <returns>PaymentDetailsDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaymentDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDetailsDTO>> GetPayment(Guid id)
        {
            var payment = await _bll.PaymentService.FindAsyncDetails(id);
            if (payment == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapDetails(payment);
        }
        
        /// <summary>
        /// Get payment by id. Only includes values used in editing
        /// </summary>
        /// <param name="id">Id of payment to get</param>
        /// <returns>PaymentEditDTO with given id</returns>
        [HttpGet("edit/{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaymentEditDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentEditDTO>> GetPaymentEdit(Guid id)
        {
            var payment = User.IsInRole(UserRole.Admin)
                ? await _bll.PaymentService.FindAsyncEdit(id)
                : await _bll.PaymentService.FindAsyncEdit(id, User.GetUserId());
            
            if (payment == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.MapEdit(payment);
        }

        /// <summary>
        /// Edit payment by id. Only comment can be edited
        /// </summary>
        /// <param name="id">Id of payment to edit</param>
        /// <param name="payment">PaymentEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPayment(Guid id, PaymentEditDTO payment)
        {
            if (id != payment.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }

            if (User.IsInRole(UserRole.Admin) 
                    ? await _bll.PaymentService.FindAsyncEdit(id) == null 
                    : await _bll.PaymentService.FindAsyncEdit(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.PaymentService.Update(_mapper.MapEdit(payment));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }
    }
}