using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ZipCode domain object.
/// </summary>
public interface IZipCodeDataAccess : IGenericDataAccess<ZipCode>
{
    /// <summary>
    /// Check if a ZipCode exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ZipCode could be found.</returns>
    Task<bool> Exists(int id);
}
