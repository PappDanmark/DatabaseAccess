using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorBatteryUpdateDataAccess : GenericDataAccess<SensorBatteryUpdate>, ISensorBatteryUpdateDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorBatteryUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorBatteryUpdateDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
