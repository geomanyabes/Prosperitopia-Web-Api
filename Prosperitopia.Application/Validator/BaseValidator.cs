using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Interface;

namespace Prosperitopia.Application.Validator
{
    public abstract class BaseValidator<T> :IBaseValidator<T> where T : class, IBaseEntity
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly string _typeName;

        public BaseValidator(IBaseRepository<T> repository)
        {
            _repository = repository;
            _typeName = typeof(T).Name;
        }

        public async virtual Task<T> ValidateOnUpdate(T entity)
        {
            if (entity.Id == 0L)
                throw new ArgumentException("ID must be specified for update.");

            var exists = await _repository.GetByIdAsync(entity.Id) ?? throw new ArgumentException("Entity with specified ID does not exist.");

            return exists;
        }

        public async virtual Task<bool> ValidateOnCreate(T entity)
        {
            return await Task.FromResult(true);
        }

        public async virtual Task<T> ValidateOnDelete(T entity)
        {
            if (entity.Id == 0L)
                throw new ArgumentException("ID must be specified for delete.");
            var exists = await _repository.GetByIdAsync(entity.Id) ?? throw new ArgumentException("Entity with specified ID does not exist.");
            return exists;
        }

        public async Task<T> ValidateOnDelete(long id)
        {
            if (id == 0L) throw new ArgumentException("Invalid id");
            return await _repository.GetByIdAsync(id) ?? throw new ArgumentException("Entity with specified ID does not exist.");
        }
    }
}
