using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaTransactionDataAccess : GenericDataAccess<ParkingAreaTransaction>, IParkingAreaTransactionDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingAreaTransactionDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ParkingAreaTransaction src, ParkingAreaTransaction dst)
    {
        dst.OccupiedSpaces  = src.OccupiedSpaces;
        dst.ParkingAreaId = src.ParkingAreaId;
        dst.Timestamp = src.Timestamp;
    }

    /// <inheritdoc/>
    public ParkingAreaTransaction? GetLastestByParkingAreaId(int id)
    {
        return this.DbContext.ParkingAreaTransactions
            .Where(e => e.ParkingAreaId.Equals(id))
            .OrderByDescending(e => e.Timestamp)
            .Take(1)
            .FirstOrDefault();
    }
}
