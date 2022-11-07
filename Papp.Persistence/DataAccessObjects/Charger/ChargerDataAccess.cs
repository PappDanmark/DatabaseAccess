using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerDataAccess : GenericDataAccess<Charger>, IChargerDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public Charger? Update(Guid id, Charger charger)
    {
        var existing = DbContext.Chargers.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.OperatorId = charger.OperatorId;
        existing.ChargerType = charger.ChargerType;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Charger?> UpdateAsync(Guid id, Charger charger)
    {
        var existing = await DbContext.Chargers.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.OperatorId = charger.OperatorId;
        existing.ChargerType = charger.ChargerType;

        return existing;
    }
}
