using Microsoft.AspNetCore.Mvc;
using Prosperitopia.DataAccess.Interface;

namespace Prosperitopia.Web.Api.Controllers
{
    public class ItemController : BaseController
    {
        //protected readonly IBaseRepository<T> _repository;

        //public BaseController(IBaseRepository<T> repository)
        //{
        //    _repository = repository;
        //}

        public virtual async Task<IActionResult> GetAll()
        {
            try
            {

                var result = await _repository.GetAll();
                ICategoryRepository
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
                var result = await _repository.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"{typeof(T).Name} with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
                //TODO: Logging
            }
        }
    }
}
