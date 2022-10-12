using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class
{
    private readonly DbContext DbContext;
    internal DbSet<T> DbSet;

    public GenericDataAccess(DbContext context)
    {
        this.DbContext = context;
        this.DbSet = context.Set<T>();
    }

    public GenericDataAccess(IUnitOfWork<DbContext> unitOfWork) : this(unitOfWork.DbContext)
    {
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<T>> GetAllAsync(IBaseSpecification<T>? specification = null)
    {
        return await SpecificationEvaluator<T>.GetQuery(DbContext.Set<T>(), specification).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<T?> GetFirstOrDefaultAsync(IBaseSpecification<T> specification)
    {
        return await SpecificationEvaluator<T>.GetQuery(DbContext.Set<T>(), specification).FirstOrDefaultAsync();
    }
}
