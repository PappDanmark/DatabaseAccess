using Papp.Persistence.Context;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.UnitOfWork;

public interface IPappDbUnitOfWork : IUnitOfWork<PappDbContext>
{
    IBoothDataAccess Booths { get; }
    IBundleDataAccess Bundles { get; }
    IChargerDataAccess Chargers { get; }
    IChargerConnectorDataAccess ChargerConnectors { get; }
    IChargerTypeDataAccess ChargerTypes { get; }
    ICountryDataAccess Countries { get; }
    IImageDataAccess Images { get; }
    IManufacturerDataAccess Manufacturers { get; }
    IOperatorDataAccess Operators { get; }
    IParkingAreaDataAccess ParkingAreas { get; }
    IParkingAreaTransactionDataAccess ParkingAreaTransactions { get; }
    IParkingBoothDataAccess ParkingBooths { get; }
    IParkingBundleDataAccess ParkingBundles { get; }
    ILegacySensorDataAccess LegacySensors { get; }
    ISensorDataAccess Sensors { get; }
    ISensorActionOccupiedDataAccess SensorActionOccupieds { get; }
    ISensorActionsRawDataAccess SensorActionsRaws { get; }
    ISensorBatteryUpdateDataAccess SensorBatteryUpdates { get; }
    ISensorInstallDataAccess SensorInstalls { get; }
    ISensorTypeDataAccess SensorTypes { get; }
    ISensorUpdateDataAccess SensorUpdates { get; }
    IZipCodeDataAccess ZipCodes { get; }
}
