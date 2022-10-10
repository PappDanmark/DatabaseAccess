using Papp.Domain;

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
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Country>(e => e.Iso3166Numeric.Equals(id)));
        return entity != null;
    }
}
