using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Application.Validator
{
    public class CategoryValidator : BaseValidator<Category>, ICategoryValidator
    {

        public CategoryValidator(ICategoryRepository repository) : base(repository)
        {
        }
        public override async Task<Category> ValidateOnUpdate(Category category)
        {
            var existingCategory = await base.ValidateOnUpdate(category);
            var hasSameName = await GetHasSameName(category.Name);

            if (hasSameName != null && hasSameName.Id != category.Id)
                throw new ArgumentException("Name must be unique.");

            return existingCategory;
        }
        public override async Task<bool> ValidateOnCreate(Category category)
        {
            var hasSameName = await GetHasSameName(category.Name);

            if (hasSameName != null)
                throw new ArgumentException("Name must be unique.");

            return true;
        }

        private async Task<Category?> GetHasSameName(string name)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
