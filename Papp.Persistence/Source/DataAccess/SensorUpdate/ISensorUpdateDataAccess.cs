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
    SensorUpdate GetLastestBySensorId(string id);
}
