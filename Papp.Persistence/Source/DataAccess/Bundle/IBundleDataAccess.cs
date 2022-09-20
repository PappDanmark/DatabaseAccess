using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Bundle domain object.
/// </summary>
public interface IBundleDataAccess : IGenericDataAccess<Bundle>
{
    /// <summary>
    /// Check if a Bundle exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Bundle could be found.</returns>
    Task<bool> Exists(int id);
}
