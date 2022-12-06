using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class LegacySensorDataAccess : GenericDataAccess<LegacySensor>, ILegacySensorDataAccess
{
    private readonly PappDbContext DbContext;

    public LegacySensorDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(LegacySensor src, LegacySensor dst)
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
