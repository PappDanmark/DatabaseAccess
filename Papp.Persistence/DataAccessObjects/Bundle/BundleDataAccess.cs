using Microsoft.EntityFrameworkCore;
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
    public Bundle? Update(int id, Bundle bundle)
    {
        var existing = DbContext.Bundles.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Location = bundle.Location;
        existing.Address = bundle.Address;
        existing.Zip = bundle.Zip;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Bundle?> UpdateAsync(int id, Bundle bundle)
    {
        var existing = await DbContext.Bundles.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Location = bundle.Location;
        existing.Address = bundle.Address;
        existing.Zip = bundle.Zip;

        return existing;
    }
}
