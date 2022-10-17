using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Country domain object.
/// </summary>
public interface ICountryDataAccess : IGenericDataAccess<Country>
{
    /// <summary>
    /// Checks asynchronously if a Country exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Country could be found.</returns>
    Task<bool> ExistsAsync(short id);
}
