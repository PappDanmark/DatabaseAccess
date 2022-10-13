using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace Papp.Persistence.DataAccess;

public class GenericDataAccess<TEntity> : IGenericDataAccess<TEntity> where TEntity : class
{
    private readonly DbContext DbContext;
    private readonly DbSet<TEntity> DbSet;

    public GenericDataAccess(DbContext context)
    {
        this.DbContext = context;
        this.DbSet = context.Set<TEntity>();
    }

    public GenericDataAccess(IUnitOfWork<DbContext> unitOfWork) : this(unitOfWork.DbContext)
    {
    }

    #region Read

    private IQueryable<TEntity> GetDbSetQueryable(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        var query = tracking ? this.DbSet : this.DbSet.AsNoTracking();

        if (include != null)
        {
            query = include(query);
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query;
    }

    /// <inheritdoc/>
    public virtual TEntity? GetFirstOrDefault(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).FirstOrDefault();
    }

    /// <inheritdoc/>
    public virtual TResult? GetFirstOrDefault<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).FirstOrDefault();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return await this.GetDbSetQueryable(predicate, orderBy, include, tracking).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return await this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public virtual IEnumerable<TEntity> GetAllAsEnumerable(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).AsEnumerable();
    }

    /// <inheritdoc/>
    public virtual IEnumerable<TResult> GetAllAsEnumerable<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).AsEnumerable();
    }

    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TEntity> GetAllAsAsyncEnumerable(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TResult> GetAllAsAsyncEnumerable<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public virtual ICollection<TEntity> GetAllAsCollection(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).ToList();
    }

    /// <inheritdoc/>
    public virtual ICollection<TResult> GetAllAsCollection<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).ToList();
    }

    /// <inheritdoc/>
    public virtual async Task<ICollection<TEntity>> GetAllAsCollectionAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false)
    {
        return await this.GetDbSetQueryable(predicate, orderBy, include, tracking).ToListAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<ICollection<TResult>> GetAllAsCollectionAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false) where TResult : class
    {
        return await this.GetDbSetQueryable(predicate, orderBy, include, tracking).Select(selector).ToListAsync();
    }

    #endregion

    #region Add

    /// <inheritdoc/>
    public virtual TEntity Add(TEntity entity)
    {
        return this.DbSet.Add(entity).Entity;
    }

    /// <inheritdoc/>
    public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
    {
        return await this.DbSet.AddAsync(entity);
    }

    /// <inheritdoc/>
    public virtual void AddRange(params TEntity[] entities)
    {
        this.DbSet.AddRange(entities);
    }

    /// <inheritdoc/>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        this.DbSet.AddRange(entities);
    }

    /// <inheritdoc/>
    public virtual async Task AddRangeAsync(params TEntity[] entities)
    {
        await this.DbSet.AddRangeAsync(entities);
    }

    /// <inheritdoc/>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await this.DbSet.AddRangeAsync(entities);
    }

    #endregion
}
