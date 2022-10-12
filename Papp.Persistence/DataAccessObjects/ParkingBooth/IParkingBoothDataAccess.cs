using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the ParkingBooth domain object.
/// </summary>
public interface IParkingBoothDataAccess : IGenericDataAccess<ParkingBooth>
{
    /// <summary>
    /// Check if a ParkingBooth exists in the DB by the given id.
    /// </summary>
    /// <param name="id">An id to check for.</param>
    /// <returns>Whether or not a matching id ParkingBooth could be found.</returns>
    Task<bool> Exists(Guid id);
}
