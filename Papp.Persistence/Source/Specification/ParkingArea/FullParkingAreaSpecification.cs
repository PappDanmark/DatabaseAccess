using System.Linq.Expressions;
using Papp.Domain;
using Papp.Persistence.DataAccess;

public class FullParkingAreaSpecification : BaseSpecification<ParkingArea>
{
    public FullParkingAreaSpecification(Expression<Func<ParkingArea, bool>> criteria, bool tracked = false) : base(criteria, tracked)
    {
        // Includes the Bundle toghether will some of it's nested properties.
        AddInclude($"{nameof(ParkingArea.ZipCode)}.{nameof(ZipCode.Country)}");
        AddInclude($"{nameof(ParkingArea.SensorType)}");
    }

    public FullParkingAreaSpecification(bool tracked = false) : this(e => true, tracked)
    {
    }
}
