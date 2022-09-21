using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ManufacturerDataAccess : GenericDataAccess<Manufacturer>, IManufacturerDataAccess
{
    private readonly PappDbContext context;

    public ManufacturerDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
    }
}
