using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorInstall domain object.
/// </summary>
public interface ISensorInstallDataAccess : IGenericDataAccess<SensorInstall>
{
    /// <summary>
    /// Retrieves all Sensor Installs, of a given Bundle id, since the provided timestamp. 
    /// Includes the last Sensor Install that crosses the provided timestamp.
    /// </summary>
    /// <param name="bundleId">The id of the Bundle from which to get the installs.</param>
    /// <param name="timestamp">The point of time at which to include the last Sensor Install.</param>
    /// <returns>A list of mathching Sensor Installs.</returns>
    Task<IEnumerable<SensorInstall>> GetAllOfBundleSince(int bundleId, DateTime timestamp);
}
