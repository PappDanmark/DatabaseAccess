using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorType domain object.
/// </summary>
public interface ISensorTypeDataAccess : IGenericDataAccess<SensorType>
{
    /// <summary>
    /// Updates a SensorType entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorType which needs to be updated.</param>
    /// <param name="sensorType">The new vesion of the SensorType used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorType entity, or null if a SensorType for the provided id doesn't exist.</returns>
    SensorType? Update(Guid id, SensorType sensorType);

    /// <summary>
    /// Updates asynchronously a SensorType entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorType which needs to be updated.</param>
    /// <param name="sensorType">The new vesion of the SensorType used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorType entity, or null if a SensorType for the provided id doesn't exist.</returns>
    Task<SensorType?> UpdateAsync(Guid id, SensorType sensorType);
}
