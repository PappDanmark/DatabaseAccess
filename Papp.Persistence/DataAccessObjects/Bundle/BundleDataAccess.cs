using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class BundleDataAccess : GenericDataAccess<Bundle>, IBundleDataAccess
{
    private readonly PappDbContext DbContext;

    public BundleDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public BundleDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
    }
}
