using Application.Common;
using Domain.Common;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class RepositoryBase<TEntity, Context> : IRepository<TEntity>
        where TEntity : class, IEntity
        where Context : DbContext
    {
        protected readonly Context _context;
        protected abstract DbSet<TEntity> EntitySet { get; }
        protected readonly IEntityGenerator _generator;

        public RepositoryBase(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public RepositoryBase(Context context, IEntityGenerator generator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default)
        {
            await EntitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> GetAsync(long id, CancellationToken cancellation = default)
        {
            return await EntitySet.FirstOrDefaultAsync(x => x.Id == id, cancellation);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
