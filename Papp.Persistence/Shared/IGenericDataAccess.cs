using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Generic interface with the purpose of defining common methods for all of the domain objects.
/// It is inherited by each data access class.
/// </summary>
public interface IGenericDataAccess<TEntity> where TEntity : class
{
    // All methods related to Read database operations:
    #region Read

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>
    /// An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.
    /// Null if there's no result data.
    /// </returns>
    TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate,
                               Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                               Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                               bool tracking = false);

    /// <summary>
    /// Gets the first or default entity with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>
    /// An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.
    /// Null if there's no result data.
    /// </returns>
    TResult? GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>> predicate,
                                        Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                        Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                        bool tracking = false) where TResult : class;

    /// <summary>
    /// Gets asynchronously the first or default entity based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>
    /// An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.
    /// Null if there's no result data.
    /// </returns>
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
                                          Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                          Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                          bool tracking = false);

    /// <summary>
    /// Gets asynchronously the first or default entity with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>
    /// An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.
    /// Null if there's no result data.
    /// </returns>
    Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                   Expression<Func<TEntity, bool>> predicate,
                                                   Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                   Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                   bool tracking = false) where TResult : class;

    /// <summary>
    /// Gets all the entities based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> with any matching entities or empty if no entities were found.</returns>
    IEnumerable<TEntity> GetAllAsEnumerable(Expression<Func<TEntity, bool>>? predicate = null,
                                            Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                            Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                            bool tracking = false);

    /// <summary>
    /// Gets all the entities with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="IEnumerable{TResult}"/> with any matching entities or empty if no entities were found.</returns>
    IEnumerable<TResult> GetAllAsEnumerable<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                     Expression<Func<TEntity, bool>>? predicate = null,
                                                     Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                     Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                     bool tracking = false) where TResult : class;

    /// <summary>
    /// Gets all the entities based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{TEntity}"/> with any matching entities or empty if no entities were found.</returns>
    IAsyncEnumerable<TEntity> GetAllAsAsyncEnumerable(Expression<Func<TEntity, bool>>? predicate = null,
                                                      Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                      Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                      bool tracking = false);

    /// <summary>
    /// Gets all the entities with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{TResult}"/> with any matching entities or empty if no entities were found.</returns>
    IAsyncEnumerable<TResult> GetAllAsAsyncEnumerable<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                               Expression<Func<TEntity, bool>>? predicate = null,
                                                               Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                               Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                               bool tracking = false) where TResult : class;

    /// <summary>
    /// Gets all the entities based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="ICollection{TEntity}"/> with any matching entities or empty if no entities were found.</returns>
    ICollection<TEntity> GetAllAsCollection(Expression<Func<TEntity, bool>>? predicate = null,
                                            Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                            Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                            bool tracking = false);

    /// <summary>
    /// Gets all the entities with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="ICollection{TResult}"/> with any matching entities or empty if no entities were found.</returns>
    ICollection<TResult> GetAllAsCollection<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                     Expression<Func<TEntity, bool>>? predicate = null,
                                                     Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                     Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                     bool tracking = false) where TResult : class;

    /// <summary>
    /// Gets asynchronously all the entities based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="ICollection{TEntity}"/> with any matching entities or empty if no entities were found.</returns>
    Task<ICollection<TEntity>> GetAllAsCollectionAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                       Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                       Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                       bool tracking = false);

    /// <summary>
    /// Gets asynchronously all the entities with projection based on a predicate, orderby delegate and include delegate.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties.</param>
    /// <param name="tracking"><c>true</c> to enable change tracking else defaults to <c>false</c>.</param>
    /// <returns>An <see cref="ICollection{TResult}"/> with any matching entities or empty if no entities were found.</returns>
    Task<ICollection<TResult>> GetAllAsCollectionAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                Expression<Func<TEntity, bool>>? predicate = null,
                                                                Func<IQueryable<TEntity>?, IOrderedQueryable<TEntity>>? orderBy = null,
                                                                Func<IQueryable<TEntity>?, IIncludableQueryable<TEntity, object>>? include = null,
                                                                bool tracking = false) where TResult : class;

    #endregion

    // All methods related to Create database operations:
    #region Add

    /// <summary>
    /// Adds the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Adds asynchronously the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds asynchronously multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    #endregion
}
