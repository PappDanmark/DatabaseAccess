using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerTypeDataAccess : GenericDataAccess<ChargerType>, IChargerTypeDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerTypeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerTypeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public ChargerType? Update(int id, ChargerType chargerType)
    {
        var existing = DbContext.ChargerTypes.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Operator = chargerType.Operator;
        existing.Kilowatt = chargerType.Kilowatt;
        existing.Dc = chargerType.Dc;
        existing.Name = chargerType.Name;
        existing.Connector = chargerType.Connector;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<ChargerType?> UpdateAsync(int id, ChargerType chargerType)
    {
        var existing = await DbContext.ChargerTypes.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Operator = chargerType.Operator;
        existing.Kilowatt = chargerType.Kilowatt;
        existing.Dc = chargerType.Dc;
        existing.Name = chargerType.Name;
        existing.Connector = chargerType.Connector;

        return existing;
    }
}
