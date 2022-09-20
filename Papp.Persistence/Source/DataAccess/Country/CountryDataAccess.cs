using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class CountryDataAccess : GenericDataAccess<Country>, ICountryDataAccess
{
    private readonly PappDbContext context;

    public CountryDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Iso3166Numeric.Equals(id));
        return entity != null;
    }
}
