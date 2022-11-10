using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class CountryDataAccess : GenericDataAccess<Country>, ICountryDataAccess
{
    private readonly PappDbContext DbContext;

    public CountryDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Country src, Country dst)
    {
        dst.CommonName = src.CommonName;
        dst.OfficialName = src.OfficialName;
        dst.Iso3166Alpha2 = src.Iso3166Alpha2;
        dst.Iso3166Alpha3 = src.Iso3166Alpha3;
        dst.Population = src.Population;
        dst.AreaKm2 = src.AreaKm2;
    }
}
