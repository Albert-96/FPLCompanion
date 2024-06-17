using System.Linq.Expressions;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IEntityRepository<T> where T : class 
    {
        ValueTask<IEnumerable<T>> GetAllAsync();
        ValueTask<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateMongoAsync(Expression<Func<T, bool>> expression, T updatedRecord);
        Task DeleteAsync(object id);
        Task Save();
    }
}
