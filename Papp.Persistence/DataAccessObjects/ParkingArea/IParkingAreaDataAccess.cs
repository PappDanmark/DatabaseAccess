using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingArea domain object.
/// </summary>
public interface IParkingAreaDataAccess : IGenericDataAccess<ParkingArea>
{
    /// <summary>
    /// Checks asynchronously if a ParkingArea exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ParkingArea could be found.</returns>
    Task<bool> ExistsAsync(int id);
}
