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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Booth src, Booth dst)
    {
        dst.BoothNumber = src.BoothNumber;
        dst.MuncipalityId = src.MuncipalityId;
        dst.HandicapOh = src.HandicapOh;
        dst.ElectricExclusiveOh = src.ElectricExclusiveOh; 
        dst.CraftsmenExclusiveOh = src.CraftsmenExclusiveOh;
        dst.Charger = src.Charger;
        dst.Bundle = src.Bundle;
        dst.SensorInstall = src.SensorInstall;
    }
}
