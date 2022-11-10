using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorTypeDataAccess : GenericDataAccess<SensorType>, ISensorTypeDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorTypeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(SensorType src, SensorType dst)
    {
        dst.Model = src.Model;
        dst.Manufacturer = src.Manufacturer;
    }
}
