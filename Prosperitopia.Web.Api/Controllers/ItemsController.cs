using Microsoft.AspNetCore.Mvc;
using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Domain.Model.Dto;

namespace Prosperitopia.Web.Api.Controllers
{
    public class ItemsController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] SearchFilter searchFilter, [FromQuery] PageFilter pageFilter)
        {
            try
            {

                var result = await _itemService.GetItems(searchFilter, pageFilter);
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
                var result = await _itemService.GetItem(id);
                if (result == null)
                {
                    return NotFound($"Item with ID {id} not found.");
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
        public async Task<IActionResult> CreateItem(ItemDto itemDto)
        {
            try
            {
                var created = await _itemService.CreateItem(itemDto);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, ItemDto itemDto)
        {
            try
            {
                itemDto.Id = id;
                var result = await _itemService.UpdateItem(itemDto);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            try
            {
                var result = await _itemService.DeleteItem(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
