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
}
