using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorTypeDataAccess : GenericDataAccess<SensorType>, ISensorTypeDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorTypeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorTypeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
