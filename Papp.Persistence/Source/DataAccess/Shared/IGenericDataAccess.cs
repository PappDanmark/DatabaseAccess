using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Generic interface with the purpose of defining common methods for all of the domain objects.
/// It is inherited by each data access class.
/// </summary>
public interface IGenericDataAccess<T> where T : class
{
    /// <summary>
    /// Adds the given entity to the DB.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    Task AddAsync(T entity);

    /// <summary>
    /// Retrieves a subset or all entities of type T from the DB.
    /// </summary>
    /// <param name="filter">A LINQ expression to be used to filter which entities will be returned.</param>
    /// <param name="tracked">Whether or not the returned entities are tracked.</param>
    /// <param name="includeProperties">A comma separated list containing case-sensitive names of any properties that should be included with every entity.</param>
    /// <returns>A list of enities of type T, that match the given filter, if no entities are found returns empty list.</returns>
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string? includeProperties = null);

    /// <summary>
    /// Retrieves the first occurunce of an entity type T from the DB.
    /// Can be used as a universal getter e.g. GetById, GetByMunicipality etc.
    /// </summary>
    /// <param name="filter">A LINQ expression to be used to filter which entity will be returned.</param>
    /// <param name="tracked">Whether or not the returned entity is tracked.</param>
    /// <param name="includeProperties">A comma separated list containing case-sensitive names of any properties that should be included with the entity.</param>
    /// <returns>An entity of type T, that matches the given filter, if none mathches returns null.</returns>
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = false, string? includeProperties = null);
}
