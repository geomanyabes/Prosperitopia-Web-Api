using Prosperitopia.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Application.Interface.Validator
{
    public interface IBaseValidator<T> where T : class, IBaseEntity
    {
        Task<bool> ValidateOnCreate(T entity);
        Task<T> ValidateOnDelete(T entity);
        Task<T> ValidateOnDelete(long id);
        Task<T> ValidateOnUpdate(T entity);
    }
}
