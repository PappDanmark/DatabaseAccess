using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaDataAccess : GenericDataAccess<ParkingArea>, IParkingAreaDataAccess
{
    private readonly PappDbContext context;

    public ParkingAreaDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ParkingArea>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
