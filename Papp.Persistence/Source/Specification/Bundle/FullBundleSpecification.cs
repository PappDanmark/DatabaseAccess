using Papp.Domain;
using Papp.Persistence.DataAccess;

public class FullBundleSpecification : BaseSpecification<Bundle>
{
    public FullBundleSpecification(bool tracked = false) : base(e => true, tracked)
    {
        // Information to include:

        // Bundle -> ZipCode information
        AddInclude(e => e.ZipNavigation);
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Id)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Code)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Name)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.CountryId)}");

        // Bundle -> ZipCode -> Country information
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Country)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Country)}.{nameof(Country.Iso3166Numeric)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Country)}.{nameof(Country.CommonName)}");
        AddInclude($"{nameof(Bundle.ZipNavigation)}.{nameof(ZipCode.Country)}.{nameof(Country.OfficialName)}");

        // Bundle -> All of it's Booths information
        AddInclude(e => e.Booths);
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.Id)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.BoothNumber)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.MuncipalityId)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.HandicapOh)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ElectricExclusiveOh)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.CraftsmenExclusiveOh)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.Charger)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.Bundle)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.SensorInstall)}");

        // Bundle -> For every Booth it's -> Charger information
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.Id)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerType)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.OperatorId)}");

        // Bundle -> For every Booth it's -> Charger -> ChargerType information
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.Id)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.Operator)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.Kilowatt)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.Name)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.Connector)}");

        // Bundle -> For every Booth it's -> Charger -> ChargerType -> ChargerConnector information
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.ConnectorNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.ConnectorNavigation)}.{nameof(ChargerConnector.Id)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.ConnectorNavigation)}.{nameof(ChargerConnector.Name)}");

        // Bundle -> For every Booth it's -> Charger -> ChargerType -> Operator information
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.OperatorNavigation)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.OperatorNavigation)}.{nameof(Operator.Id)}");
        AddInclude($"{nameof(Bundle.Booths)}.{nameof(Booth.ChargerNavigation)}.{nameof(Charger.ChargerTypeNavigation)}.{nameof(ChargerType.OperatorNavigation)}.{nameof(Operator.Name)}");
    }
}
