using Microsoft.EntityFrameworkCore;
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

    public CountryDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public Country? Update(short id, Country country)
    {
        var existing = DbContext.Countries.FirstOrDefault(e => e.Iso3166Numeric == id);

        if (existing == null)
        {
            return null;
        }

        existing.CommonName = country.CommonName;
        existing.OfficialName = country.OfficialName;
        existing.Iso3166Alpha2 = country.Iso3166Alpha2;
        existing.Iso3166Alpha3 = country.Iso3166Alpha3;
        existing.Population = country.Population;
        existing.AreaKm2 = country.AreaKm2;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Country?> UpdateAsync(short id, Country country)
    {
        var existing = await DbContext.Countries.FirstOrDefaultAsync(e => e.Iso3166Numeric == id);

        if (existing == null)
        {
            return null;
        }

        existing.CommonName = country.CommonName;
        existing.OfficialName = country.OfficialName;
        existing.Iso3166Alpha2 = country.Iso3166Alpha2;
        existing.Iso3166Alpha3 = country.Iso3166Alpha3;
        existing.Population = country.Population;
        existing.AreaKm2 = country.AreaKm2;

        return existing;
    }
}
