using Prosperitopia.Domain.Model.Dto;

namespace Prosperitopia.Application.Interface.Service
{
    public interface IItemService
    {
        Task<ItemDto> CreateItem(ItemDto item);
        Task<ItemDto> GetItem(long id);
        Task<PagedResult<ItemDto>> GetItems(SearchFilter searchFilter, PageFilter pageFilter);
        Task<ItemDto> UpdateItem(ItemDto item);
    }
}
