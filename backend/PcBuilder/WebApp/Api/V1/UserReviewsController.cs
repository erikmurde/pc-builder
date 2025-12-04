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
using Public.DTO.V1.UserReview;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing user reviews
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserReviewsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserReviewMapper _mapper;

        /// <summary>
        /// Constructor for user reviews controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public UserReviewsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserReviewMapper(mapper);
        }

        /// <summary>
        /// Get list of user reviews
        /// </summary>
        /// <returns>List of UserReviewDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<UserReviewDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReviewDTO>>> GetUserReviews()
        {
            var userReviews = await _bll.UserReviewService.AllAsync();

            return userReviews
                .Select(r => _mapper.Map(r))
                .ToList();
        }
        
        /// <summary>
        /// Get list of user reviews belonging to the user
        /// </summary>
        /// <returns>List of UserReviewDTOs belonging to the user</returns>
        [HttpGet("user")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<UserReviewDTO>), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<UserReviewDTO>>> GetUserReviewsByUserId()
        {
            var userReviews = await _bll.UserReviewService.AllAsync(User.GetUserId());

            return userReviews
                .Select(r => _mapper.Map(r))
                .ToList();
        }

        /// <summary>
        /// Get user review by id
        /// </summary>
        /// <param name="id">Id of user review to get</param>
        /// <returns>UserReviewDTO</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserReviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReviewDTO>> GetUserReview(Guid id)
        {
            var userReview = await _bll.UserReviewService.FindAsync(id);
            if (userReview == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }
            
            return _mapper.Map(userReview);
        }

        /// <summary>
        /// Edit user review by id. Review must belong to the user
        /// </summary>
        /// <param name="id">Id of user review to edit</param>
        /// <param name="userReview">UserReviewEditDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutUserReview(Guid id, UserReviewEditDTO userReview)
        {
            if (id != userReview.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.UserReviewService.FindAsync(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.UserReviewService.Update(_mapper.MapEdit(userReview));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return NoContent();
        }

        /// <summary>
        /// Create new user review
        /// </summary>
        /// <param name="userReview">UserReviewCreateDTO with values of new user review</param>
        /// <returns>UserReviewCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserReviewCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserReviewCreateDTO>> PostUserReview(UserReviewCreateDTO userReview)
        {
            var bllUserReview = _mapper.MapCreate(userReview);

            try
            {
                bllUserReview = _bll.UserReviewService.Add(bllUserReview, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }

            return CreatedAtAction("GetUserReview", new { id = bllUserReview.Id }, bllUserReview);
        }

        /// <summary>
        /// Delete user review by id. Review must belong to the user
        /// </summary>
        /// <param name="id">Id of user review to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUserReview(Guid id)
        {
            if (await _bll.UserReviewService.FindAsync(id, User.GetUserId()) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.UserReviewService.RemoveAsync(id);
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