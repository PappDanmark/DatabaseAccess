namespace Papp.Persistence.DataAccess;

public interface IDataAccessFactory
{    
    /// <summary>
    /// Gets the specified data access object for the <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of type inherited from <see cref="IGenericDataAccess{TEntity}"/> interface.</returns>
    IGenericDataAccess<TEntity> GenericDataAccessObject<TEntity>() where TEntity : class;
}
