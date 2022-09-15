using Papp.Domain;

namespace Papp.Persistence.DataAccess;

public class BoothDataAccess : GenericDataAccess<Booth>, IBoothDataAccess
{
    private readonly PappDbContext context;

    public BoothDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
    }
}
