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
    public async Task<bool> ExistsAsync(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.ParkingBoothId.Equals(id));
        return entity != null;
    }
}
