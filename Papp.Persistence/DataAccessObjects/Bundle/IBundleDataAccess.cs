using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Bundle domain object.
/// </summary>
public interface IBundleDataAccess : IGenericDataAccess<Bundle>
{
    /// <summary>
    /// Updates a Bundle entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Bundle which needs to be updated.</param>
    /// <param name="bundle">The new vesion of the Bundle used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Bundle entity, or null if a Bundle for the provided id doesn't exist.</returns>
    Bundle? Update(int id, Bundle bundle);

    /// <summary>
    /// Updates asynchronously a Bundle entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Bundle which needs to be updated.</param>
    /// <param name="bundle">The new vesion of the Bundle used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Bundle entity, or null if a Bundle for the provided id doesn't exist.</returns>
    Task<Bundle?> UpdateAsync(int id, Bundle bundle);
}
