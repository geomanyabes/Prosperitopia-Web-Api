using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Extension;
using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.Application.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Dto;
using Prosperitopia.Domain.Model.Entity;
using Prosperitopia.Domain.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Prosperitopia.Application.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemValidator _itemValidator;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IItemValidator itemValidator, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _itemValidator = itemValidator;
            _mapper = mapper;
        }

        public async Task<PagedResult<ItemDto>> GetItems(SearchFilter searchFilter, PageFilter pageFilter)
        {
            var query = _itemRepository.GetAll().AsQueryable();

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
            int pageSize = pageFilter.PageSize;
            query = query.OrderByString(pageFilter.SortProperty, pageFilter.SortDirection)
                .Skip(page * pageSize).Take(pageSize);
            var totalCount = await query.CountAsync();
            var items = await query.ToListAsync();
            var mapped = _mapper.Map<List<ItemDto>>(items);
            return new PagedResult<ItemDto>(pageSize, totalCount, pageFilter.Page, mapped);
        }

        public async Task<ItemDto> GetItem(long id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<ItemDto> UpdateItem(ItemDto item)
        {
            var mapped = _mapper.Map<Item>(item);

            var existing = await _itemValidator.ValidateOnUpdate(mapped);

            existing.Name = item.Name;
            existing.Description = item.Description;
            existing.Category = item.Category;
            existing.Price = item.Price;
            existing.ModifiedDate = DateTime.Now;
            existing.ModifiedBy = "test";
            await _itemRepository.SaveChangesAsync();
            return _mapper.Map<ItemDto>(existing);
        }
        public async Task<ItemDto> CreateItem(ItemDto item)
        {
            var mapped = _mapper.Map<Item>(item);
            mapped.CreatedDate = DateTime.Now;
            mapped.CreatedBy = "test";

            _ = await _itemValidator.ValidateOnCreate(mapped);

            _itemRepository.Insert(mapped);
            await _itemRepository.SaveChangesAsync();
            return _mapper.Map<ItemDto>(mapped);
        }

        public async Task<ItemDto> DeleteItem(long id)
        {
            var item = await _itemValidator.ValidateOnDelete(id);
            _itemRepository.Update(item, new()
            {
                IsDeleted = true,
                ModifiedBy = "test",
                ModifiedDate = DateTime.Now
            });
            await _itemRepository.SaveChangesAsync();
            return _mapper.Map<ItemDto>(item);
        }
    }
}
