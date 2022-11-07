using Microsoft.EntityFrameworkCore;
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

    /// <inheritdoc/>
    public Sensor? Update(string id, Sensor sensor)
    {
        var existing = DbContext.Sensors.FirstOrDefault(e => e.SensorId == id);

        if (existing == null)
        {
            return null;
        }

        existing.InstallationTimestamp = sensor.InstallationTimestamp;
        existing.Battery = sensor.Battery;
        existing.Occupied = sensor.Occupied;
        existing.LastUpdatedBySensorAction = sensor.LastUpdatedBySensorAction;
        existing.LastUpdatedTimestamp = sensor.LastUpdatedTimestamp;
        existing.InstalledAtParkingBoothId = sensor.InstalledAtParkingBoothId;
        existing.InstallationDateTimeEpoch = sensor.InstallationDateTimeEpoch;
        existing.SensorTypeId = sensor.SensorTypeId;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Sensor?> UpdateAsync(string id, Sensor sensor)
    {
        var existing = await DbContext.Sensors.FirstOrDefaultAsync(e => e.SensorId == id);

        if (existing == null)
        {
            return null;
        }

        existing.InstallationTimestamp = sensor.InstallationTimestamp;
        existing.Battery = sensor.Battery;
        existing.Occupied = sensor.Occupied;
        existing.LastUpdatedBySensorAction = sensor.LastUpdatedBySensorAction;
        existing.LastUpdatedTimestamp = sensor.LastUpdatedTimestamp;
        existing.InstalledAtParkingBoothId = sensor.InstalledAtParkingBoothId;
        existing.InstallationDateTimeEpoch = sensor.InstallationDateTimeEpoch;
        existing.SensorTypeId = sensor.SensorTypeId;

        return existing;
    }
}
