using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Extension;
using Prosperitopia.Application.Interface;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Dto;
using Prosperitopia.Domain.Model.Enum;

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

        public async Task<List<ItemDto>> GetItems(SearchFilter searchFilter, PageFilter pageFilter)
        {
            var query = _itemRepository.GetAll()
                .Include(x => x.Category).AsQueryable();

            string search = searchFilter.Search;
            
            if(!string.IsNullOrWhiteSpace(search))
            {
                switch (searchFilter.SearchType)
                {
                    case SearchType.CONTAINS:
                        query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%") || EF.Functions.Like(x.Description, $"%{search}%"));
                        break;
                    case SearchType.STARTS_WITH: 
                        query = query.Where(x => EF.Functions.Like(x.Name, $"{search}%") || EF.Functions.Like(x.Description, $"{search}%"));
                        break;
                    case SearchType.ENDS_WITH:
                        query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}") || EF.Functions.Like(x.Description, $"%{search}"));
                        break;
                    case SearchType.EXACT:
                    default:
                        query = query.Where(x => EF.Functions.Like(x.Name, search) || EF.Functions.Like(x.Description, search));
                        break;

                }
            }
            int page = pageFilter.Page - 1;
            query = query.OrderByString(pageFilter.SortProperty, pageFilter.SortDirection)
                .Skip(pageFilter.Page *)
        }
    }
}
