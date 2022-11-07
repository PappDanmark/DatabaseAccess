using Microsoft.EntityFrameworkCore;
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

    public ParkingBoothDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public ParkingBooth? Update(Guid id, ParkingBooth parkingBooth)
    {
        var existing = DbContext.ParkingBooths.FirstOrDefault(e => e.ParkingBoothId == id);

        if (existing == null)
        {
            return null;
        }

        existing.BoothNumber = parkingBooth.BoothNumber;
        existing.PoiId = parkingBooth.PoiId;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<ParkingBooth?> UpdateAsync(Guid id, ParkingBooth parkingBooth)
    {
        var existing = await DbContext.ParkingBooths.FirstOrDefaultAsync(e => e.ParkingBoothId == id);

        if (existing == null)
        {
            return null;
        }

        existing.BoothNumber = parkingBooth.BoothNumber;
        existing.PoiId = parkingBooth.PoiId;

        return existing;
    }
}
