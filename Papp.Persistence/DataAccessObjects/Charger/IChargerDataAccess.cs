using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Charger domain object.
/// </summary>
public interface IChargerDataAccess : IGenericDataAccess<Charger>
{
    /// <summary>
    /// Updates a Charger entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Charger which needs to be updates.</param>
    /// <param name="charger">The new vesion of the Charger used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Charger entity, or null if the a Charger for the provided id doesn't exist.</returns>
    Charger? Update(Guid id, Charger charger);

    /// <summary>
    /// Updates asynchronously a Charger entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Charger which needs to be updates.</param>
    /// <param name="charger">The new vesion of the Charger used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Charger entity, or null if the a Charger for the provided id doesn't exist.</returns>
    Task<Charger?> UpdateAsync(Guid id, Charger charger);
}
