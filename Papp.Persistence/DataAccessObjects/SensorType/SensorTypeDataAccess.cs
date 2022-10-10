using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorTypeDataAccess : GenericDataAccess<SensorType>, ISensorTypeDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorTypeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorTypeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<SensorType>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
