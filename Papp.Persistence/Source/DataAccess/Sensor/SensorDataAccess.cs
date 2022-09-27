using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorDataAccess : GenericDataAccess<Sensor>, ISensorDataAccess
{
    private readonly PappDbContext context;

    public SensorDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(string id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Sensor>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
