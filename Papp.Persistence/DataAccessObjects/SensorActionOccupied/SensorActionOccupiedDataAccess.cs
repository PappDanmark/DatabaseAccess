using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorActionOccupiedDataAccess : GenericDataAccess<SensorActionOccupied>, ISensorActionOccupiedDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorActionOccupiedDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(SensorActionOccupied src, SensorActionOccupied dst)
    {
        dst.SensorId = src.SensorId;
        dst.Occupied = src.Occupied;
        dst.ActionTimestamp = src.ActionTimestamp;
    }
}
