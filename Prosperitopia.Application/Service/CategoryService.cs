using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Extension;
using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Dto;
using Prosperitopia.Domain.Model.Entity;
using Prosperitopia.Domain.Model.Enum;

namespace Prosperitopia.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryValidator _categoryValidator;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryValidator categoryValidator, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetCategories(SearchFilter searchFilter, PageFilter? pageFilter)
        {
            var query = _categoryRepository.GetAll()
                .AsQueryable();

            string search = searchFilter.Search;

            if (!string.IsNullOrWhiteSpace(search))
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

            var categorys = await query.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categorys);
        }

        public async Task<CategoryDto> GetCategory(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> CreateCategory(CategoryDto category)
        {
            var mapped = _mapper.Map<Category>(category);

            _ = await _categoryValidator.ValidateOnCreate(mapped);

            _categoryRepository.Insert(mapped);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(mapped);
        }
    }
}
