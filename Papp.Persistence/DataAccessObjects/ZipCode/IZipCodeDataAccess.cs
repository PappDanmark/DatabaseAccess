using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ZipCode domain object.
/// </summary>
public interface IZipCodeDataAccess : IGenericDataAccess<ZipCode>
{
    /// <summary>
    /// Updates a ZipCode entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ZipCode which needs to be updated.</param>
    /// <param name="zipCode">The new vesion of the ZipCode used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ZipCode entity, or null if a ZipCode for the provided id doesn't exist.</returns>
    ZipCode? Update(int id, ZipCode zipCode);

    /// <summary>
    /// Updates asynchronously a ZipCode entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ZipCode which needs to be updated.</param>
    /// <param name="zipCode">The new vesion of the ZipCode used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ZipCode entity, or null if a ZipCode for the provided id doesn't exist.</returns>
    Task<ZipCode?> UpdateAsync(int id, ZipCode zipCode);
}
