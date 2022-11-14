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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Charger src, Charger dst)
    {
        dst.OperatorId = src.OperatorId;
        dst.ChargerType = src.ChargerType;
    }
}
