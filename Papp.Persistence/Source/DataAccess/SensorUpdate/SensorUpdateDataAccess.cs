using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorUpdateDataAccess : GenericDataAccess<SensorUpdate>, ISensorUpdateDataAccess
{
    private readonly PappDbContext context;

    public SensorUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public SensorUpdate? GetLastestBySensorId(string id)
    {
        return this.context.SensorUpdates
            .Where(e => e.SensorId.Equals(id))
            .OrderByDescending(e => e.Ts)
            .Take(1)
            .FirstOrDefault();
    }
}
