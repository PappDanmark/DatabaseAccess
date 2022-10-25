using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaDataAccess : GenericDataAccess<ParkingArea>, IParkingAreaDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingAreaDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ParkingAreaDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
