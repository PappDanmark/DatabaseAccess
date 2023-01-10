using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingBundleDataAccess : GenericDataAccess<ParkingBundle>, IParkingBundleDataAccess
{
    private readonly PappDbContext DbContext;

    public ParkingBundleDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ParkingBundle src, ParkingBundle dst)
    {
        dst.Location = src.Location;
        dst.Address = src.Address;
        dst.Zip = src.Zip;
    }
}
