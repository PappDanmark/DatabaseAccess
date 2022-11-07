using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorBatteryUpdate domain object.
/// </summary>
public interface ISensorBatteryUpdateDataAccess : IGenericDataAccess<SensorBatteryUpdate>
{
    /// <summary>
    /// Updates a SensorBatteryUpdate entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorBatteryUpdate which needs to be updated.</param>
    /// <param name="sensorBatteryUpdate">The new vesion of the SensorBatteryUpdate used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorBatteryUpdate entity, or null if a SensorBatteryUpdate for the provided id doesn't exist.</returns>
    SensorBatteryUpdate? Update(Guid id, SensorBatteryUpdate sensorBatteryUpdate);

    /// <summary>
    /// Updates asynchronously a SensorBatteryUpdate entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorBatteryUpdate which needs to be updated.</param>
    /// <param name="sensorBatteryUpdate">The new vesion of the SensorBatteryUpdate used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorBatteryUpdate entity, or null if a SensorBatteryUpdate for the provided id doesn't exist.</returns>
    Task<SensorBatteryUpdate?> UpdateAsync(Guid id, SensorBatteryUpdate sensorBatteryUpdate);
}
