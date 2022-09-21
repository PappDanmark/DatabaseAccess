using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Sensor domain object.
/// </summary>
public interface ISensorDataAccess : IGenericDataAccess<Sensor>
{
    /// <summary>
    /// Check if a Sensor exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Sensor could be found.</returns>
    Task<bool> Exists(string id);
}
