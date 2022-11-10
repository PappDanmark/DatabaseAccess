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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Bundle src, Bundle dst)
    {
        dst.Location = src.Location;
        dst.Address = src.Address;
        dst.Zip = src.Zip;
    }
}
