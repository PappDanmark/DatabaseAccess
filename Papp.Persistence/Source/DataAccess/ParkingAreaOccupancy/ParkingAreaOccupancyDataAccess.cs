using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ParkingAreaOccupancyDataAccess : GenericDataAccess<ParkingAreaOccupancy>, IParkingAreaOccupancyDataAccess
{
    private readonly PappDbContext context;

    public ParkingAreaOccupancyDataAccess(PappDbContext context) : base(context)
    {
        this.context = context;
    }
}
