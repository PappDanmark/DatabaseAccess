using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ZipCodeDataAccess : GenericDataAccess<ZipCode>, IZipCodeDataAccess
{
    private readonly PappDbContext context;

    public ZipCodeDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ZipCode>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
