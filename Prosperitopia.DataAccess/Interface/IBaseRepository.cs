using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Prosperitopia.DataAccess.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        void Insert(TEntity entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void Update(TEntity existing, TEntity newValues);
    }
}
