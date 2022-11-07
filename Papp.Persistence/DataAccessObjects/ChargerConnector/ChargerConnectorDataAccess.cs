using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ChargerConnectorDataAccess : GenericDataAccess<ChargerConnector>, IChargerConnectorDataAccess
{
    private readonly PappDbContext DbContext;

    public ChargerConnectorDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ChargerConnectorDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public ChargerConnector? Update(short id, ChargerConnector chargerConnector)
    {
        var existing = DbContext.ChargerConnectors.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = chargerConnector.Name;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<ChargerConnector?> UpdateAsync(short id, ChargerConnector chargerConnector)
    {
        var existing = await DbContext.ChargerConnectors.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = chargerConnector.Name;

        return existing;
    }
}
