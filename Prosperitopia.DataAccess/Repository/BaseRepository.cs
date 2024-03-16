using Microsoft.EntityFrameworkCore;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain;
using Prosperitopia.Domain.Interface;
using System.Linq.Expressions;

namespace Prosperitopia.DataAccess.Repository
{

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly ProsperitopiaDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ProsperitopiaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await GetAll().Where(filter).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(TEntity existing, TEntity newValues)
        {
            var entry = _dbContext.Entry(existing);
            entry.CurrentValues.SetValues(newValues);
            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
