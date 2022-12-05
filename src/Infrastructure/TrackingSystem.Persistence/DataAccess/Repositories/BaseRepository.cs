using Microsoft.EntityFrameworkCore;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Abstract;

namespace JustCommerce.Persistence.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
          where TEntity : Entity, new()
    {
        protected DbSet<TEntity> _Entities;
        public BaseRepository(DbSet<TEntity> entities)
        {
            _Entities = entities;
        }
        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _Entities.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _Entities.AddRangeAsync(entities, cancellationToken);
        }

        public virtual Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _Entities.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _Entities.ToListAsync(cancellationToken);
        }

        public virtual ValueTask<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _Entities.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _Entities.AsQueryable();
        }

        public virtual void Remove(TEntity entity)
        {
            _Entities.Remove(entity);
        }

        public virtual void RemoveById(Guid id)
        {
            var attachedEntity = _Entities.Attach(new TEntity { Id = id });
            attachedEntity.State = EntityState.Deleted;
        }

        public virtual void Update(TEntity entity)
        {
            _Entities.Update(entity);
        }
    }
}
