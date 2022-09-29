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
    /// <param name="specification">A specification class containg any filters for which kind of entities will be returned, or null for all the entities.</param>
    /// <returns>A list of enities of type T, that match the given filter, if no entities are found returns empty list.</returns>
    Task<IList<T>> GetAllAsync(IBaseSpecification<T>? specification = null);

    /// <summary>
    /// Retrieves the first occurunce of an entity type T from the DB.
    /// Can be used as a universal getter e.g. GetById, GetByMunicipality etc.
    /// </summary>
    /// <param name="specification">A specification class containg any filters for which kind of entities will be returned.</param>
    /// <returns>An entity of type T, that matches the given filter, if none mathches returns null.</returns>
    Task<T?> GetFirstOrDefaultAsync(IBaseSpecification<T> specification);
}
