using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ChargerConnector domain object.
/// </summary>
public interface IChargerConnectorDataAccess : IGenericDataAccess<ChargerConnector>
{
    /// <summary>
    /// Check if a ChargerConnector exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ChargerConnector could be found.</returns>
    Task<bool> Exists(short id);
}
