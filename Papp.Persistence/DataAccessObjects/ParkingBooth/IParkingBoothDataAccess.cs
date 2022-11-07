using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingBooth domain object.
/// </summary>
public interface IParkingBoothDataAccess : IGenericDataAccess<ParkingBooth>
{
    /// <summary>
    /// Updates a ParkingBooth entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ParkingBooth which needs to be updated.</param>
    /// <param name="parkingBooth">The new vesion of the ParkingBooth used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ParkingBooth entity, or null if a ParkingBooth for the provided id doesn't exist.</returns>
    ParkingBooth? Update(Guid id, ParkingBooth parkingBooth);

    /// <summary>
    /// Updates asynchronously a ParkingBooth entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the ParkingBooth which needs to be updated.</param>
    /// <param name="parkingBooth">The new vesion of the ParkingBooth used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified ParkingBooth entity, or null if a ParkingBooth for the provided id doesn't exist.</returns>
    Task<ParkingBooth?> UpdateAsync(Guid id, ParkingBooth parkingBooth);
}
