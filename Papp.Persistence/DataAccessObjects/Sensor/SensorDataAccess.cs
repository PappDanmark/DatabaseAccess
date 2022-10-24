using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorDataAccess : GenericDataAccess<Sensor>, ISensorDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
