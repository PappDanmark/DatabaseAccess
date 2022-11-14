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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ChargerConnector src, ChargerConnector dst)
    {
        dst.Name = src.Name;
    }
}
