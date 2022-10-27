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
}
