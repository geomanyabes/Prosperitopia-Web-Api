using Prosperitopia.Domain.Model.Dto;

namespace Prosperitopia.Application.Interface.Service
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategory(CategoryDto category);
        Task<List<CategoryDto>> GetCategories(SearchFilter searchFilter, PageFilter? pageFilter);
        Task<CategoryDto> GetCategory(long id);
    }
}
