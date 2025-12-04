using System.Net.Mime;
using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using DAL.EF.Base;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.V1;
using Public.DTO.V1.Category;

namespace WebApp.Api.V1
{
    /// <summary>
    /// Controller for managing categories
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper;

        /// <summary>
        /// Constructor for categories controller
        /// </summary>
        /// <param name="bll">Wrapper for services</param>
        /// <param name="mapper">AutoMapper baseclass for mapping DTOs</param>
        public CategoriesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CategoryMapper(mapper);
        }

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns>List of CategoryDTOs</returns>
        [HttpGet("")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _bll.CategoryService.AllAsync();

            return categories
                .Select(c => _mapper.Map(c))
                .ToList();
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">Id of category to get</param>
        /// <returns>CategoryDTO with given id</returns>
        [HttpGet("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _bll.CategoryService.FindAsync(id);
            if (category == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            return _mapper.Map(category);
        }

        /// <summary>
        /// Edit category by id
        /// </summary>
        /// <param name="id">Id of category to edit</param>
        /// <param name="category">CategoryDTO with new values</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest(EntityErrorHelper.EntitiesNotMatchingError());
            }
            if (await _bll.CategoryService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                _bll.CategoryService.Update(_mapper.Map(category));
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }
            
            return NoContent();
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">CategoryCreateDTO with values of new category</param>
        /// <returns>CategoryCreateDTO with created values</returns>
        [HttpPost]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryCreateDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<ActionResult<CategoryCreateDTO>> PostCategory(CategoryCreateDTO category)
        {
            var bllCategory = _mapper.MapCreate(category);

            try
            {
                bllCategory = _bll.CategoryService.Add(bllCategory);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                return BadRequest(EntityErrorHelper.InvalidEntityError());
            }
            
            return CreatedAtAction("GetCategory", new { id = bllCategory.Id }, bllCategory);
        }

        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id">Id of category to delete</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (await _bll.CategoryService.FindAsync(id) == null)
            {
                return NotFound(EntityErrorHelper.CannotFetchEntityError());
            }

            try
            {
                await _bll.CategoryService.RemoveAsync(id);
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