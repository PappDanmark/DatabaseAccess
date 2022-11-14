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
        dst.InstallationTimestamp = src.InstallationTimestamp;
        dst.Battery = src.Battery;
        dst.Occupied = src.Occupied;
        dst.LastUpdatedBySensorAction = src.LastUpdatedBySensorAction;
        dst.LastUpdatedTimestamp = src.LastUpdatedTimestamp;
        dst.InstalledAtParkingBoothId = src.InstalledAtParkingBoothId;
        dst.InstallationDateTimeEpoch = src.InstallationDateTimeEpoch;
        dst.SensorTypeId = src.SensorTypeId;
    }
}
