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
}
