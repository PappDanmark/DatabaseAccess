using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Manufacturer domain object.
/// </summary>
public interface IManufacturerDataAccess : IGenericDataAccess<Manufacturer>
{
    /// <summary>
    /// Checks asynchronously if a Manufacturer exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Manufacturer could be found.</returns>
    Task<bool> ExistsAsync(short id);
}
