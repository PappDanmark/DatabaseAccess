using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerTypeDataAccess : GenericDataAccess<ChargerType>, IChargerTypeDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerTypeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerTypeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ChargerType>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
