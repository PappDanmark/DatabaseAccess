using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorUpdateDataAccess : GenericDataAccess<SensorUpdate>, ISensorUpdateDataAccess
{
    private readonly PappDbContext context;

    public SensorUpdateDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }
}
