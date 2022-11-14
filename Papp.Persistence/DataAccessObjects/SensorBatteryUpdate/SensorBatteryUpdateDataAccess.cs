using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorBatteryUpdateDataAccess : GenericDataAccess<SensorBatteryUpdate>, ISensorBatteryUpdateDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorBatteryUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(SensorBatteryUpdate src, SensorBatteryUpdate dst)
    {
        dst.SensorId = src.SensorId;
        dst.Ts = src.Ts;
        dst.Battery = src.Battery;
    }
}
