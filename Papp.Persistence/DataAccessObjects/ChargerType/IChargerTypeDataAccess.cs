using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ChargerType domain object.
/// </summary>
public interface IChargerTypeDataAccess : IGenericDataAccess<ChargerType>
{
    /// <summary>
    /// Checks asynchronously if a ChargerType exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ChargerType could be found.</returns>
    Task<bool> ExistsAsync(int id);
}
