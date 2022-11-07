using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorInstallDataAccess : GenericDataAccess<SensorInstall>, ISensorInstallDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorInstallDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorInstallDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SensorInstall>> GetAllOfBundleSince(int bundleId, DateTime timestamp)
    {
        return await this.DbContext.SensorInstalls
        .Where(e =>
            // Any Sensor Installs that references the target Bundle.
            e.BoothNavigation.Bundle == bundleId &&
            // Any Sensor Installs whose Uninstall Timestamp is null or later then the target timestamp.
            (e.UninstallTs == null || e.UninstallTs.Value.CompareTo(timestamp) > 0)
        )
        .Include(e => e.BoothNavigation)
        .ToListAsync();
    }

    /// <inheritdoc/>
    public SensorInstall? Update(int id, SensorInstall sensorInstall)
    {
        var existing = DbContext.SensorInstalls.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.InstallTs = sensorInstall.InstallTs;
        existing.UninstallTs = sensorInstall.UninstallTs;
        existing.InstallImage = sensorInstall.InstallImage;
        existing.Booth = sensorInstall.Booth;
        existing.SensorId = sensorInstall.SensorId;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<SensorInstall?> UpdateAsync(int id, SensorInstall sensorInstall)
    {
        var existing = await DbContext.SensorInstalls.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.InstallTs = sensorInstall.InstallTs;
        existing.UninstallTs = sensorInstall.UninstallTs;
        existing.InstallImage = sensorInstall.InstallImage;
        existing.Booth = sensorInstall.Booth;
        existing.SensorId = sensorInstall.SensorId;

        return existing;
    }
}
