using System.Linq.Expressions;
using Papp.Domain;
using Papp.Persistence.DataAccess;

public class FullBundleSpecification : BaseSpecification<Bundle>
{
    public FullBundleSpecification(Expression<Func<Bundle, bool>> criteria, bool tracked = false) : base(criteria, tracked)
    {
        // Includes the Bundle toghether will some of it's nested properties.
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Country)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.SensorInstallNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.ConnectorNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.OperatorNavigation)}");
    }

    public FullBundleSpecification(bool tracked = false) : this(e => true, tracked)
    {
    }
}
