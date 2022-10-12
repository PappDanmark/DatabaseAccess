using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerDataAccess : GenericDataAccess<Charger>, IChargerDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Charger>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
