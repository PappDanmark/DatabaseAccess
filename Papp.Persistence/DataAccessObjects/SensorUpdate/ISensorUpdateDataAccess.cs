using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorUpdate domain object.
/// </summary>
public interface ISensorUpdateDataAccess : IGenericDataAccess<SensorUpdate>
{
    /// <summary>
    /// Retrieves last Sensor Update of a specific sensor id.
    /// </summary>
    /// <returns>Latest Sensor Update.</returns>
    SensorUpdate? GetLastestBySensorId(string id);

    /// <summary>
    /// Retrieves all Sensor Updates for a given Booth id, since a specific point in time till present.
    /// </summary>
    /// <param name="boothId">The id of the Booth from which to get the updates.</param>
    /// <param name="timestamp">The point of time since when to retrieve the updates.</param>
    /// <returns>A list of mathching Sensor Updates.</returns>
    IEnumerable<SensorUpdate> GetAllByBoothIdSince(Guid boothId, DateTime timestamp);

    /// <summary>
    /// Retrieves all Sensor Updates of all Sensor Installs, of a given Bundle id, since the provided timestamp. 
    /// Includes updates from the last Sensor Install that crosses the provided timestamp.
    /// </summary>
    /// <param name="boothId">The id of the Booth from which to get the updates.</param>
    /// <param name="timestamp">The point of time which to detect the last Sensor Install which to take the updates from.</param>
    /// <param name="withBoothId">Whether to overwrite the Sensor Update id with the id of the Booth from which the update came from.</param>
    /// <returns>A list of mathching Sensor Updates.</returns>
    Task<IEnumerable<SensorUpdate>> GetAllByBundleIdOfSensorInstallsSinceAsync(int bundleId, DateTime timestamp, bool withBoothId = false);
}
