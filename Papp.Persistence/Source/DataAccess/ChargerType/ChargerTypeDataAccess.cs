using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerTypeDataAccess : GenericDataAccess<ChargerType>, IChargerTypeDataAccess
{
    private readonly PappDbContext context;

    public ChargerTypeDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ChargerType>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
