using Papp.Domain;
using Papp.Persistence.Context;

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
}
