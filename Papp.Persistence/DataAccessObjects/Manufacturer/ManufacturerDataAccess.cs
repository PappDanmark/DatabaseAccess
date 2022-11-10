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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Manufacturer src, Manufacturer dst)
    {
        dst.Name = src.Name;
    }
}
