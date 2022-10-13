using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Image domain object.
/// </summary>
public interface IImageDataAccess : IGenericDataAccess<Image>
{
    /// <summary>
    /// Checks asynchronously if a Image exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id Image could be found.</returns>
    Task<bool> ExistsAsync(int id);
}
