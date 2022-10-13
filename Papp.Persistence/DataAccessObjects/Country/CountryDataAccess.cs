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
    public async Task<bool> ExistsAsync(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Iso3166Numeric.Equals(id));
        return entity != null;
    }
}
