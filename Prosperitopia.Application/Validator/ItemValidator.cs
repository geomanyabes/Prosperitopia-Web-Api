using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Application.Validator
{
    public class ItemValidator : BaseValidator<Item>, IItemValidator
    {

        public ItemValidator(IItemRepository repository) : base(repository)
        {
        }
        public override async Task<Item> ValidateOnUpdate(Item item)
        {
            var existingItem = await base.ValidateOnUpdate(item);
            var hasSameName = await GetHasSameName(item.Name);

            if (hasSameName != null && hasSameName.Id != item.Id)
                throw new ArgumentException("Name must be unique.");

            return existingItem;
        }
        public override async Task<bool> ValidateOnCreate(Item item)
        {
            var hasSameName = await GetHasSameName(item.Name);

            if (hasSameName != null)
                throw new ArgumentException("Name must be unique.");

            return true;
        }

        private async Task<Item?> GetHasSameName(string name)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
