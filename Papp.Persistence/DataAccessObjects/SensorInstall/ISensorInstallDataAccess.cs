using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorInstall domain object.
/// </summary>
public interface ISensorInstallDataAccess : IGenericDataAccess<SensorInstall>
{
    /// <summary>
    /// Updates a SensorInstall entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorInstall which needs to be updated.</param>
    /// <param name="sensorInstall">The new vesion of the SensorInstall used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorInstall entity, or null if a SensorInstall for the provided id doesn't exist.</returns>
    SensorInstall? Update(int id, SensorInstall sensorInstall);

    /// <summary>
    /// Updates asynchronously a SensorInstall entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the SensorInstall which needs to be updated.</param>
    /// <param name="sensorInstall">The new vesion of the SensorInstall used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified SensorInstall entity, or null if a SensorInstall for the provided id doesn't exist.</returns>
    Task<SensorInstall?> UpdateAsync(int id, SensorInstall sensorInstall);

    /// <summary>
    /// Retrieves all Sensor Installs, of a given Bundle id, since the provided timestamp. 
    /// Includes the last Sensor Install that crosses the provided timestamp.
    /// </summary>
    /// <param name="bundleId">The id of the Bundle from which to get the installs.</param>
    /// <param name="timestamp">The point of time at which to include the last Sensor Install.</param>
    /// <returns>A list of mathching Sensor Installs.</returns>
    Task<IEnumerable<SensorInstall>> GetAllOfBundleSince(int bundleId, DateTime timestamp);
}
