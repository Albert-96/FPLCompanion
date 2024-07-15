using System.Linq.Expressions;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IEntityRepository<T> where T : class 
    {
        ValueTask<List<T>> GetAllAsync();
        ValueTask<T> GetByIdAsync(object id);
        ValueTask<List<T>> GetFilterAsync(Expression<Func<T, bool>> expression);
        Task InsertAsync(T entity);
        Task UpdateMongoAsync(Expression<Func<T, bool>> expression, T updatedRecord);
        Task DeleteAsync(object id);
        Task Save();
    }
}
