using Domain.Common;

namespace Domain.Data
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<T> GetAsync(long id);
        Task<T> AddAsync(T entity);
    }
}
