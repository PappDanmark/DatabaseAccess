using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaTransactionDataAccess : GenericDataAccess<ParkingAreaTransaction>, IParkingAreaTransactionDataAccess
{
    private readonly PappDbContext context;

    public ParkingAreaTransactionDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ParkingAreaTransaction>(e => e.Id.Equals(id)));
        return entity != null;
    }

    /// <inheritdoc/>
    public ParkingAreaTransaction? GetLastestByParkingAreaId(int id)
    {
        return this.context.ParkingAreaTransactions
            .Where(e => e.ParkingAreaId.Equals(id))
            .OrderByDescending(e => e.Timestamp)
            .Take(1)
            .FirstOrDefault();
    }
}
