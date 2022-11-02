using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorUpdateDataAccess : GenericDataAccess<SensorUpdate>, ISensorUpdateDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorUpdateDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public SensorUpdate? GetLastestBySensorId(string id)
    {
        return this.DbContext.SensorUpdates
            .Where(e => e.SensorId.Equals(id))
            .OrderByDescending(e => e.Ts)
            .Take(1)
            .FirstOrDefault();
    }

    /// <inheritdoc/>
    public IEnumerable<SensorUpdate> GetAllByBoothIdSince(Guid boothId, DateTime timestamp)
    {
        return this.DbContext.SensorInstalls
        .Where(e =>
            // BoothIds have to match.
            e.Booth == boothId &&
            // Either uninstall TS is null, or uninstall TS is after request TS.
            (e.UninstallTs == null || e.UninstallTs.Value.CompareTo(timestamp) > 0)
        )
        .AsEnumerable()
        .SelectMany(e =>
            this.DbContext.SensorUpdates
            .Where(x =>
                x.SensorId == e.SensorId &&
                x.Ts.CompareTo(e.InstallTs) > 0 &&
                x.Ts.CompareTo(timestamp) > 0 &&
                x.Ts.CompareTo(e.UninstallTs ?? DateTime.UtcNow) < 0
            )
        )
        .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SensorUpdate>> GetAllByBundleIdOfSensorInstallsSinceAsync(int bundleId, DateTime timestamp)
    {
        return await this.DbContext.SensorInstalls
        .Where(e =>
            // Any Sensor Installs that references the target Bundle.
            e.BoothNavigation.Bundle == bundleId &&
            // Any Sensor Installs that have Uninstall Timestamp null or later then the target timestamp.
            (e.UninstallTs == null || e.UninstallTs.Value.CompareTo(timestamp) > 0)
        )
        .Include(e => e.BoothNavigation)
        .SelectMany(sensorInstall =>
            this.DbContext.SensorUpdates
            .Where(sensorUpdate =>
                // Retrieves all Sensor Updates of a given Sensor Install.
                sensorUpdate.SensorId == sensorInstall.SensorId &&
                sensorUpdate.Ts.CompareTo(sensorInstall.InstallTs) > 0 &&
                sensorUpdate.Ts.CompareTo(sensorInstall.UninstallTs ?? DateTime.UtcNow) < 0
            )
            .Select(sensorUpdate =>
                new SensorUpdate() 
                {
                    Id = sensorInstall.Booth,
                    SensorId = sensorUpdate.SensorId,
                    Ts = sensorUpdate.Ts,
                    Occupied = sensorUpdate.Occupied
                }
            )
        )
        // Sort in order of the Sensor Updates timestamps.
        .OrderBy(sensorUpdate => sensorUpdate.Ts)
        .ToListAsync();
    }
}
