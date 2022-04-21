using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public abstract class RepositoryBase<TEntity, Context> : IRepository<TEntity>
        where TEntity : class, IEntity
        where Context : DbContext
    {
        protected readonly Context _context;
        protected abstract DbSet<TEntity> EntitySet { get; }

        public RepositoryBase(Context context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await EntitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> GetAsync(long id)
        {
            return await EntitySet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
