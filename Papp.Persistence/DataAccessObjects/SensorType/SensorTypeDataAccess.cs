using Microsoft.EntityFrameworkCore;
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

    public SensorTypeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public SensorType? Update(Guid id, SensorType sensorType)
    {
        var existing = DbContext.SensorTypes.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Model = sensorType.Model;
        existing.Manufacturer = sensorType.Manufacturer;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<SensorType?> UpdateAsync(Guid id, SensorType sensorType)
    {
        var existing = await DbContext.SensorTypes.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Model = sensorType.Model;
        existing.Manufacturer = sensorType.Manufacturer;

        return existing;
    }
}
