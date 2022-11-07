using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Operator domain object.
/// </summary>
public interface IOperatorDataAccess : IGenericDataAccess<Operator>
{
    /// <summary>
    /// Updates a Operator entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Operator which needs to be updated.</param>
    /// <param name="operator">The new vesion of the Operator used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Operator entity, or null if a Operator for the provided id doesn't exist.</returns>
    Operator? Update(short id, Operator operatorEntity);

    /// <summary>
    /// Updates asynchronously a Operator entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Operator which needs to be updated.</param>
    /// <param name="operator">The new vesion of the Operator used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Operator entity, or null if a Operator for the provided id doesn't exist.</returns>
    Task<Operator?> UpdateAsync(short id, Operator operatorEntity);
}
