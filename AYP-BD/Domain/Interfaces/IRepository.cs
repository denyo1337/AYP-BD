using Domain.Common;

namespace Domain.Data
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<T> GetAsync(long id, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
