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
        .ToList()
        .SelectMany(e => {
            // For each of the appropriate sensor install, query it's sensor updates.
            DateTime beginTimestamp = e.InstallTs.CompareTo(timestamp) > 0 ? e.InstallTs : timestamp;

            return this.DbContext.SensorUpdates.Where(x =>
                x.SensorId == e.SensorId &&
                x.Ts.CompareTo(beginTimestamp) > 0 &&
                x.Ts.CompareTo(e.UninstallTs ?? DateTime.UtcNow) < 0
            ).AsEnumerable();
        });
    }
}
