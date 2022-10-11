using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerConnectorDataAccess : GenericDataAccess<ChargerConnector>, IChargerConnectorDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerConnectorDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerConnectorDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ChargerConnector>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
