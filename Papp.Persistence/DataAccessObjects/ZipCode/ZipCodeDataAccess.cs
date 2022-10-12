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

    public ZipCodeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<ZipCode>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
