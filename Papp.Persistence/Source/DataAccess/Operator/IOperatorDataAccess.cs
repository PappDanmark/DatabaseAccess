using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Operator domain object.
/// </summary>
public interface IOperatorDataAccess : IGenericDataAccess<Operator>
{
    /// <summary>
    /// Check if a Operator exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Operator could be found.</returns>
    Task<bool> Exists(short id);
}
