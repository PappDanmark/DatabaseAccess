using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorActionsRawDataAccess : GenericDataAccess<SensorActionsRaw>, ISensorActionsRawDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorActionsRawDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(SensorActionsRaw src, SensorActionsRaw dst)
    {
        dst.SensorAction = src.SensorAction;
    }
}
