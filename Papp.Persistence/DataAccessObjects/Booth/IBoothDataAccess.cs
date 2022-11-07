using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Booth domain object.
/// </summary>
public interface IBoothDataAccess : IGenericDataAccess<Booth>
{
    /// <summary>
    /// Updates a Booth entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Booth which needs to be updated.</param>
    /// <param name="booth">The new vesion of the Booth used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Booth entity, or null if a Booth for the provided id doesn't exist.</returns>
    Booth? Update(Guid id, Booth booth);

    /// <summary>
    /// Updates asynchronously a Booth entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Booth which needs to be updated.</param>
    /// <param name="booth">The new vesion of the Booth used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Booth entity, or null if a Booth for the provided id doesn't exist.</returns>
    Task<Booth?> UpdateAsync(Guid id, Booth booth);
}
