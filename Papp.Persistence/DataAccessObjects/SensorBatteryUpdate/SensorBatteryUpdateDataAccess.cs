using Microsoft.EntityFrameworkCore;
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

    public SensorBatteryUpdateDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public SensorBatteryUpdate? Update(Guid id, SensorBatteryUpdate sensorBatteryUpdate)
    {
        var existing = DbContext.SensorBatteryUpdates.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.SensorId = sensorBatteryUpdate.SensorId;
        existing.Ts = sensorBatteryUpdate.Ts;
        existing.Battery = sensorBatteryUpdate.Battery;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<SensorBatteryUpdate?> UpdateAsync(Guid id, SensorBatteryUpdate sensorBatteryUpdate)
    {
        var existing = await DbContext.SensorBatteryUpdates.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.SensorId = sensorBatteryUpdate.SensorId;
        existing.Ts = sensorBatteryUpdate.Ts;
        existing.Battery = sensorBatteryUpdate.Battery;

        return existing;
    }
}
