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
    public async Task<bool> Exists(string id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Sensor>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
