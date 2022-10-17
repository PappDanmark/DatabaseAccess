using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class OperatorDataAccess : GenericDataAccess<Operator>, IOperatorDataAccess
{
    private readonly PappDbContext DbContext;

    public OperatorDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public OperatorDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity != null;
    }
}
