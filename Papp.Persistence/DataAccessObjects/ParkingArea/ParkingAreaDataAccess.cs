using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaDataAccess : GenericDataAccess<ParkingArea>, IParkingAreaDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingAreaDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ParkingAreaDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public ParkingArea? Update(int id, ParkingArea parkingArea)
    {
        var existing = DbContext.ParkingAreas.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.TotalSpaces = parkingArea.TotalSpaces;
        existing.Coordinates = parkingArea.Coordinates;
        existing.ZipCodeId = parkingArea.ZipCodeId;
        existing.SensorTypeId = parkingArea.SensorTypeId;
        existing.PappId = parkingArea.PappId;
        existing.CoordinatesEntry = parkingArea.CoordinatesEntry;
        existing.Street = parkingArea.Street;
        existing.Name = parkingArea.Name;
        existing.LatestOccupiedSpaces = parkingArea.LatestOccupiedSpaces;
        existing.LatestOccupiedTimestamp = parkingArea.LatestOccupiedTimestamp;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<ParkingArea?> UpdateAsync(int id, ParkingArea parkingArea)
    {
        var existing = await DbContext.ParkingAreas.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.TotalSpaces = parkingArea.TotalSpaces;
        existing.Coordinates = parkingArea.Coordinates;
        existing.ZipCodeId = parkingArea.ZipCodeId;
        existing.SensorTypeId = parkingArea.SensorTypeId;
        existing.PappId = parkingArea.PappId;
        existing.CoordinatesEntry = parkingArea.CoordinatesEntry;
        existing.Street = parkingArea.Street;
        existing.Name = parkingArea.Name;
        existing.LatestOccupiedSpaces = parkingArea.LatestOccupiedSpaces;
        existing.LatestOccupiedTimestamp = parkingArea.LatestOccupiedTimestamp;

        return existing;
    }
}
