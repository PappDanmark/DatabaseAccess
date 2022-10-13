using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorType domain object.
/// </summary>
public interface ISensorTypeDataAccess : IGenericDataAccess<SensorType>
{
    /// <summary>
    /// Checks asynchronously if a SensorType exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id SensorType could be found.</returns>
    Task<bool> ExistsAsync(Guid id);
}
