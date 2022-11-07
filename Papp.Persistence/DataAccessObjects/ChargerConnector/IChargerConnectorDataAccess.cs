using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ChargerConnector domain object.
/// </summary>
public interface IChargerConnectorDataAccess : IGenericDataAccess<ChargerConnector>
{
    /// <summary>
    /// Updates a ChargerConnector entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ChargerConnector which needs to be updated.</param>
    /// <param name="chargerConnector">The new vesion of the ChargerConnector used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ChargerConnector entity, or null if a ChargerConnector for the provided id doesn't exist.</returns>
    ChargerConnector? Update(short id, ChargerConnector chargerConnector);

    /// <summary>
    /// Updates asynchronously a ChargerConnector entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ChargerConnector which needs to be updated.</param>
    /// <param name="chargerConnector">The new vesion of the ChargerConnector used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ChargerConnector entity, or null if a ChargerConnector for the provided id doesn't exist.</returns>
    Task<ChargerConnector?> UpdateAsync(short id, ChargerConnector chargerConnector);
}
