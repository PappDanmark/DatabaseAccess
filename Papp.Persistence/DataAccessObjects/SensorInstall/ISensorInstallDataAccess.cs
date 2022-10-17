using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the SensorInstall domain object.
/// </summary>
public interface ISensorInstallDataAccess : IGenericDataAccess<SensorInstall>
{
    /// <summary>
    /// Checks asynchronously if a SensorInstall exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id SensorInstall could be found.</returns>
    Task<bool> ExistsAsync(int id);
}
