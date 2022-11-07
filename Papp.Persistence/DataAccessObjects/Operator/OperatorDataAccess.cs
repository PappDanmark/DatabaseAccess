using Microsoft.EntityFrameworkCore;
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
    public Operator? Update(short id, Operator operatorEntity)
    {
        var existing = DbContext.Operators.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = operatorEntity.Name;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Operator?> UpdateAsync(short id, Operator operatorEntity)
    {
        var existing = await DbContext.Operators.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = operatorEntity.Name;

        return existing;
    }
}
