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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ParkingArea src, ParkingArea dst)
    {
        dst.TotalSpaces = src.TotalSpaces;
        dst.Coordinates = src.Coordinates;
        dst.ZipCodeId = src.ZipCodeId;
        dst.SensorTypeId = src.SensorTypeId;
        dst.PappId = src.PappId;
        dst.CoordinatesEntry = src.CoordinatesEntry;
        dst.Street = src.Street;
        dst.Name = src.Name;
        dst.LatestOccupiedSpaces = src.LatestOccupiedSpaces;
        dst.LatestOccupiedTimestamp = src.LatestOccupiedTimestamp;
    }
}
