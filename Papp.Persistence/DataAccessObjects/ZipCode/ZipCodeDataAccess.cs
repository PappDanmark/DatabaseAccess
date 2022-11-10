using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ZipCodeDataAccess : GenericDataAccess<ZipCode>, IZipCodeDataAccess
{
    private readonly PappDbContext DbContext;

    public ZipCodeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ZipCode src, ZipCode dst)
    {
        dst.Code = src.Code;
        dst.Name = src.Name;
        dst.CountryId = src.CountryId;
    }
}
