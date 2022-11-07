using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingArea domain object.
/// </summary>
public interface IParkingAreaDataAccess : IGenericDataAccess<ParkingArea>
{
    /// <summary>
    /// Updates a ParkingArea entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ParkingArea which needs to be updated.</param>
    /// <param name="parkingArea">The new vesion of the ParkingArea used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ParkingArea entity, or null if a ParkingArea for the provided id doesn't exist.</returns>
    ParkingArea? Update(int id, ParkingArea parkingArea);

    /// <summary>
    /// Updates asynchronously a ParkingArea entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ParkingArea which needs to be updated.</param>
    /// <param name="parkingArea">The new vesion of the ParkingArea used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ParkingArea entity, or null if a ParkingArea for the provided id doesn't exist.</returns>
    Task<ParkingArea?> UpdateAsync(int id, ParkingArea parkingArea);
}
