using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Charger domain object.
/// </summary>
public interface IChargerDataAccess : IGenericDataAccess<Charger>
{
    /// <summary>
    /// Check if a Charger exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Charger could be found.</returns>
    Task<bool> Exists(Guid id);
}
