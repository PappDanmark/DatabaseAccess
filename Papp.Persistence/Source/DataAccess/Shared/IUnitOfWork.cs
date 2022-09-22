namespace Papp.Persistence.DataAccess;

public interface IUnitOfWork
{
    IBoothDataAccess BoothDataAccess{ get; }
    IBundleDataAccess BundleDataAccess{ get; }
    IChargerDataAccess ChargerDataAccess{ get; }
    IChargerConnectorDataAccess ChargerConnectorDataAccess{ get; }
    IChargerTypeDataAccess ChargerTypeDataAccess{ get; }
    ICountryDataAccess CountryDataAccess{ get; }
    IImageDataAccess ImageDataAccess{ get; }
    IManufacturerDataAccess ManufacturerDataAccess{ get; }
    IOperatorDataAccess OperatorDataAccess{ get; }
    IParkingAreaDataAccess ParkingAreaDataAccess{ get; }
    IParkingAreaTransactionDataAccess ParkingAreaTransactionDataAccess{ get; }
    IParkingBoothDataAccess ParkingBoothDataAccess{ get; }
    ISensorDataAccess SensorDataAccess{ get; }
    ISensorBatteryUpdateDataAccess SensorBatteryUpdateDataAccess{ get; }
    ISensorInstallDataAccess SensorInstallDataAccess{ get; }
    ISensorTypeDataAccess SensorTypeDataAccess{ get; }
    ISensorUpdateDataAccess SensorUpdateDataAccess{ get; }

    Task SaveChangesAsync();
}
