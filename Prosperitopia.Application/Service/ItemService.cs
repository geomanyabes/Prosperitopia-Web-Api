using AutoMapper;
using Prosperitopia.Application.Interface;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Dto;

namespace Prosperitopia.Application.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> GetItems(SearchFilter filter, PageFilter pageFilter)
        {

        }
    }
}
