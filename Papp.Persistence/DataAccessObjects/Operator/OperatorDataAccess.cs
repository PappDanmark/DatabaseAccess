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
}
