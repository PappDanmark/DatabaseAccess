using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public interface IGenericDataAccess<T> where T : class
{
    Task AddAsync(T entity);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string? includeProperties = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = false, string? includeProperties = null);
}
