using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingBoothDataAccess : GenericDataAccess<ParkingBooth>, IParkingBoothDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingBoothDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ParkingBooth src, ParkingBooth dst)
    {
        dst.BoothNumber = src.BoothNumber;
        dst.PoiId = src.PoiId;
    }
}
