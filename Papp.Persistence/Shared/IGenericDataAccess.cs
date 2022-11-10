using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
    TResult? FirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                     Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
    Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                Expression<Func<TEntity, bool>>? predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                                bool tracking = false) where TResult : class;

    #endregion

    // All methods related to Create database operations:
    #region Add

    /// <summary>
    /// Adds the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>The entity with any modified fields.</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Adds asynchronously the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>The entity with any modified fields.</returns>
    Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    void AddRange(params TEntity[] entities);

    /// <summary>
    /// Adds multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds asynchronously multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    Task AddRangeAsync(params TEntity[] entities);

    /// <summary>
    /// Adds asynchronously multiple entities to the database.
    /// </summary>
    /// <param name="entities">The set of entities to be added.</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    #endregion

    // All methods related to Update database operations:
    #region Update

    /// <summary>
    /// Updates the first <see cref="TEntity"/> in the database that matches the predicate.
    /// </summary>
    /// <param name="predicate">Predicate expression based on which to look for the <see cref="TEntity"/> to update.</param>
    /// <param name="entity">The <see cref="TEntity"/> containing the new information that needs to be overwritten.</param>
    void Update(Expression<Func<TEntity, bool>> predicate, TEntity entity);

    /// <summary>
    /// Updates asynchronously the first <see cref="TEntity"/> in the database that matches the predicate.
    /// </summary>
    /// <param name="predicate">Predicate expression based on which to look for the <see cref="TEntity"/> to update.</param>
    /// <param name="entity">The <see cref="TEntity"/> containing the new information that needs to be overwritten.</param>
    Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);

    #endregion

    // Other methods related to miscellaneous database operations:
    #region Other

    /// <summary>
    /// Checks if any entity exists in the database by the given predicate
    /// or if the database table has any entities at all.
    /// </summary>
    /// <param name="predicate">Predicate expression based on which to check.</param>
    /// <returns>
    /// Whether or not a matching entity could be found or if the predicate 
    /// is null whether the database table contains any entities at all.
    /// </returns>
    bool Exists(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Checks asynchronously if any entity exists in the database by the given predicate
    /// or if the database table has any entities at all.
    /// </summary>
    /// <param name="predicate">Predicate expression based on which to check.</param>
    /// <returns>
    /// Whether or not a matching entity could be found or if the predicate 
    /// is null whether the database table contains any entities at all.
    /// </returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate = null);

    #endregion
}
