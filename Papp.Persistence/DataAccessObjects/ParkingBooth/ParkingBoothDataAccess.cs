using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingBoothDataAccess : GenericDataAccess<ParkingBooth>, IParkingBoothDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingBoothDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ParkingBoothDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
