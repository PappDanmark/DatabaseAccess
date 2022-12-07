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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Sensor src, Sensor dst)
    {
        dst.Type = src.Type;
        dst.LatestOccupied = src.LatestOccupied;
        dst.LatestOccupiedTimestamp = src.LatestOccupiedTimestamp;
        dst.LatestBattery = src.LatestBattery;
        dst.LatestBatteryTimestamp = src.LatestBatteryTimestamp;
    }
}
