using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingBoothDataAccess : GenericDataAccess<ParkingBooth>, IParkingBoothDataAccess
{
    private readonly PappDbContext context;

    public ParkingBoothDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.ParkingBoothId.Equals(id));
        return entity != null;
    }
}
