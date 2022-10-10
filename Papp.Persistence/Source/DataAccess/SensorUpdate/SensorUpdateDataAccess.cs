using Papp.Domain;

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
}
