using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerConnectorDataAccess : GenericDataAccess<ChargerConnector>, IChargerConnectorDataAccess
{
    private readonly PappDbContext context;

    public ChargerConnectorDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ChargerConnector>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
