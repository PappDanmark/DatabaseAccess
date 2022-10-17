using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingAreaTransaction domain object.
/// </summary>
public interface IParkingAreaTransactionDataAccess : IGenericDataAccess<ParkingAreaTransaction>
{
    /// <summary>
    /// Checks asynchronously if a ParkingAreaTransaction exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ParkingAreaTransaction could be found.</returns>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    /// Retrieves last Parking Area Transaction of a specific Parking Area id.
    /// </summary>
    /// <returns>Latest Parking Area Transaction.</returns>
    ParkingAreaTransaction? GetLastestByParkingAreaId(int id);
}
