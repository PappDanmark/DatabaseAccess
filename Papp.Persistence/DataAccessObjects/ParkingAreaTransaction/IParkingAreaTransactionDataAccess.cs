using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingAreaTransaction domain object.
/// </summary>
public interface IParkingAreaTransactionDataAccess : IGenericDataAccess<ParkingAreaTransaction>
{
    /// <summary>
    /// Retrieves last Parking Area Transaction of a specific Parking Area id.
    /// </summary>
    /// <returns>Latest Parking Area Transaction.</returns>
    ParkingAreaTransaction? GetLastestByParkingAreaId(int id);
}
