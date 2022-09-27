using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class OperatorDataAccess : GenericDataAccess<Operator>, IOperatorDataAccess
{
    private readonly PappDbContext context;

    public OperatorDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(short id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Operator>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
