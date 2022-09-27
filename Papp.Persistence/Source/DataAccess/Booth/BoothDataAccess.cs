using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class BoothDataAccess : GenericDataAccess<Booth>, IBoothDataAccess
{
    private readonly PappDbContext context;

    public BoothDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Booth>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
