using Microsoft.AspNetCore.Mvc;
using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Domain.Model.Dto;

namespace Prosperitopia.Web.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] SearchFilter searchFilter, [FromQuery] PageFilter pageFilter)
        {
            try
            {

                var result = await _categoryService.GetCategories(searchFilter, pageFilter);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
                //TODO: Logging
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await _categoryService.GetCategory(id);
                if (result == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
                //TODO: Logging
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                var created = await _categoryService.CreateCategory(categoryDto);
                return Ok(created);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
                //TODO: Logging
            }
        }
    }
}
