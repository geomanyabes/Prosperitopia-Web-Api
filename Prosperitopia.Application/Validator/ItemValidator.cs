using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Application.Validator
{
    public class ItemValidator : BaseValidator<Item>, IItemValidator
    {
        private readonly ICategoryRepository _categoryRepository;

        public ItemValidator(IItemRepository repository, ICategoryRepository categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }
        public override async Task<Item> ValidateOnUpdate(Item item)
        {
            var existingItem = await base.ValidateOnUpdate(item);
            var hasSameName = await GetHasSameName(item.Name);

            if (hasSameName != null && hasSameName.Id != item.Id)
                throw new ArgumentException("Name must be unique.");

            if (item.CategoryId.HasValue && !(await CategoryIdExists(item.CategoryId.Value)))
                throw new ArgumentException("Category must exist.");

            return existingItem;
        }
        public override async Task<bool> ValidateOnCreate(Item item)
        {
            var hasSameName = await GetHasSameName(item.Name);

            if (hasSameName != null)
                throw new ArgumentException("Name must be unique.");

            if (item.CategoryId.HasValue && !(await CategoryIdExists(item.CategoryId.Value)))
                throw new ArgumentException("Category must exist.");

            return true;
        }
        private async Task<bool> CategoryIdExists(long categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category != null; 
        }

        private async Task<Item?> GetHasSameName(string name)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
