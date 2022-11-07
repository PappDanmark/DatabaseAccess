using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class BoothDataAccess : GenericDataAccess<Booth>, IBoothDataAccess
{
    private readonly PappDbContext DbContext;

    public BoothDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public BoothDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public Booth? Update(Guid id, Booth booth)
    {
        var existing = DbContext.Booths.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.BoothNumber = booth.BoothNumber;
        existing.MuncipalityId = booth.MuncipalityId;
        existing.HandicapOh = booth.HandicapOh;
        existing.ElectricExclusiveOh = booth.ElectricExclusiveOh;
        existing.CraftsmenExclusiveOh = booth.CraftsmenExclusiveOh;
        existing.Charger = booth.Charger;
        existing.Bundle = booth.Bundle;
        existing.SensorInstall = booth.SensorInstall;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Booth?> UpdateAsync(Guid id, Booth booth)
    {
        var existing = await DbContext.Booths.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.BoothNumber = booth.BoothNumber;
        existing.MuncipalityId = booth.MuncipalityId;
        existing.HandicapOh = booth.HandicapOh;
        existing.ElectricExclusiveOh = booth.ElectricExclusiveOh;
        existing.CraftsmenExclusiveOh = booth.CraftsmenExclusiveOh;
        existing.Charger = booth.Charger;
        existing.Bundle = booth.Bundle;
        existing.SensorInstall = booth.SensorInstall;

        return existing;
    }
}
