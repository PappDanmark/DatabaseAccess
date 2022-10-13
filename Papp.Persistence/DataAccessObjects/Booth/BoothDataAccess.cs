using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class BoothDataAccess : GenericDataAccess<Booth>, IBoothDataAccess
{
    private readonly PappDbContext DbContext;

    public BoothDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public BoothDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Guid id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
    }
}
