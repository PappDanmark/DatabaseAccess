using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Booth domain object.
/// </summary>
public interface IBoothDataAccess : IGenericDataAccess<Booth>
{
    /// <summary>
    /// Checks asynchronously if a Booth exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Booth could be found.</returns>
    Task<bool> ExistsAsync(Guid id);
}
