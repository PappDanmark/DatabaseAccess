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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(SensorInstall src, SensorInstall dst)
    {
        dst.InstallTs = src.InstallTs;
        dst.UninstallTs = src.UninstallTs;
        dst.InstallImage = src.InstallImage;
        dst.Booth = src.Booth;
        dst.SensorId = src.SensorId;
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
}
