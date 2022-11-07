using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ChargerType domain object.
/// </summary>
public interface IChargerTypeDataAccess : IGenericDataAccess<ChargerType>
{
    /// <summary>
    /// Updates a ChargerType entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ChargerType which needs to be updated.</param>
    /// <param name="chargerType">The new vesion of the ChargerType used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ChargerType entity, or null if a ChargerType for the provided id doesn't exist.</returns>
    ChargerType? Update(int id, ChargerType chargerType);

    /// <summary>
    /// Updates asynchronously a ChargerType entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ChargerType which needs to be updated.</param>
    /// <param name="chargerType">The new vesion of the ChargerType used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ChargerType entity, or null if a ChargerType for the provided id doesn't exist.</returns>
    Task<ChargerType?> UpdateAsync(int id, ChargerType chargerType);
}
