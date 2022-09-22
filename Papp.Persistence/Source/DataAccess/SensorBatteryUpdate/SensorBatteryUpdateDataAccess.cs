using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorBatteryUpdateDataAccess : GenericDataAccess<SensorBatteryUpdate>, ISensorBatteryUpdateDataAccess
{
    private readonly PappDbContext context;

    public SensorBatteryUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }
}
