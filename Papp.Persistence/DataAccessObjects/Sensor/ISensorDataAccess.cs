using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Sensor domain object.
/// </summary>
public interface ISensorDataAccess : IGenericDataAccess<Sensor>
{
    /// <summary>
    /// Updates a Sensor entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Sensor which needs to be updated.</param>
    /// <param name="operator">The new vesion of the Sensor used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Sensor entity, or null if a Sensor for the provided id doesn't exist.</returns>
    Sensor? Update(string id, Sensor sensor);

    /// <summary>
    /// Updates asynchronously a Sensor entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Sensor which needs to be updated.</param>
    /// <param name="operator">The new vesion of the Sensor used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Sensor entity, or null if a Sensor for the provided id doesn't exist.</returns>
    Task<Sensor?> UpdateAsync(string id, Sensor sensor);
}
