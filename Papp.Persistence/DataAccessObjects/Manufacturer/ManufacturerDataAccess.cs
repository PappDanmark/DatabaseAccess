using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ManufacturerDataAccess : GenericDataAccess<Manufacturer>, IManufacturerDataAccess
{
    private readonly PappDbContext DbContext;

    public ManufacturerDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ManufacturerDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Manufacturer>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
