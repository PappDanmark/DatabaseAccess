using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Manufacturer domain object.
/// </summary>
public interface IManufacturerDataAccess : IGenericDataAccess<Manufacturer>
{
    /// <summary>
    /// Updates a Manufacturer entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Manufacturer which needs to be updated.</param>
    /// <param name="manufacturer">The new vesion of the Manufacturer used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Manufacturer entity, or null if a Manufacturer for the provided id doesn't exist.</returns>
    Manufacturer? Update(short id, Manufacturer manufacturer);

    /// <summary>
    /// Updates asynchronously a Manufacturer entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Manufacturer which needs to be updated.</param>
    /// <param name="manufacturer">The new vesion of the Manufacturer used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Manufacturer entity, or null if a Manufacturer for the provided id doesn't exist.</returns>
    Task<Manufacturer?> UpdateAsync(short id, Manufacturer manufacturer);
}
