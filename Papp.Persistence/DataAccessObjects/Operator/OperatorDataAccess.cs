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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Operator src, Operator dst)
    {
        dst.Name = src.Name;
    }
}
