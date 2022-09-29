using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class
{
    private readonly PappDbContext context;
    internal DbSet<T> dbSet;

    public GenericDataAccess(PappDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<T>> GetAllAsync(IBaseSpecification<T>? specification = null)
    {
        return await SpecificationEvaluator<T>.GetQuery(context.Set<T>(), specification).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<T?> GetFirstOrDefaultAsync(IBaseSpecification<T> specification)
    {
        return await SpecificationEvaluator<T>.GetQuery(context.Set<T>(), specification).FirstOrDefaultAsync();
    }
}
