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

    public ParkingAreaTransactionDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
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
