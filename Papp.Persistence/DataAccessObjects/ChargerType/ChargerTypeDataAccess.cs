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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(ChargerType src, ChargerType dst)
    {
        dst.Operator = src.Operator;
        dst.Kilowatt = src.Kilowatt;
        dst.Dc = src.Dc;
        dst.Name = src.Name;
        dst.Connector = src.Connector;
    }
}
