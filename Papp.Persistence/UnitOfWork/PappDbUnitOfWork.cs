using Microsoft.Extensions.Logging;
using Papp.Persistence.Context;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.UnitOfWork;

public class PappDbUnitOfWork : UnitOfWork<PappDbContext>, IPappDbUnitOfWork
{
    private Lazy<BoothDataAccess> lazyBoothDataAccess;
    private Lazy<BundleDataAccess> lazyBundleDataAccess;
    private Lazy<ChargerDataAccess> lazyChargerDataAccess;
    private Lazy<ChargerConnectorDataAccess> lazyChargerConnectorDataAccess;
    private Lazy<ChargerTypeDataAccess> lazyChargerTypeDataAccess;
    private Lazy<CountryDataAccess> lazyCountryDataAccess;
    private Lazy<ImageDataAccess> lazyImageDataAccess;
    private Lazy<ManufacturerDataAccess> lazyManufacturerDataAccess;
    private Lazy<OperatorDataAccess> lazyOperatorDataAccess;
    private Lazy<ParkingAreaDataAccess> lazyParkingAreaDataAccess;
    private Lazy<ParkingAreaTransactionDataAccess> lazyParkingAreaTransactionDataAccess;
    private Lazy<ParkingBoothDataAccess> lazyParkingBoothDataAccess;
    private Lazy<ParkingBundleDataAccess> lazyParkingBundleDataAccess;
    private Lazy<SensorDataAccess> lazySensorDataAccess;
    private Lazy<SensorActionOccupiedDataAccess> lazySensorActionOccupiedDataAccess;
    private Lazy<SensorActionsRawDataAccess> lazySensorActionsRawDataAccess;
    private Lazy<SensorBatteryUpdateDataAccess> lazySensorBatteryUpdateDataAccess;
    private Lazy<SensorInstallDataAccess> lazySensorInstallDataAccess;
    private Lazy<SensorTypeDataAccess> lazySensorTypeDataAccess;
    private Lazy<SensorUpdateDataAccess> lazySensorUpdateDataAccess;
    private Lazy<ZipCodeDataAccess> lazyZipCodeDataAccess;

    public IBoothDataAccess Booths => lazyBoothDataAccess.Value;
    public IBundleDataAccess Bundles => lazyBundleDataAccess.Value;
    public IChargerDataAccess Chargers => lazyChargerDataAccess.Value;
    public IChargerConnectorDataAccess ChargerConnectors => lazyChargerConnectorDataAccess.Value;
    public IChargerTypeDataAccess ChargerTypes => lazyChargerTypeDataAccess.Value;
    public ICountryDataAccess Countries => lazyCountryDataAccess.Value;
    public IImageDataAccess Images => lazyImageDataAccess.Value;
    public IManufacturerDataAccess Manufacturers => lazyManufacturerDataAccess.Value;
    public IOperatorDataAccess Operators => lazyOperatorDataAccess.Value;
    public IParkingAreaDataAccess ParkingAreas => lazyParkingAreaDataAccess.Value;
    public IParkingAreaTransactionDataAccess ParkingAreaTransactions => lazyParkingAreaTransactionDataAccess.Value;
    public IParkingBoothDataAccess ParkingBooths => lazyParkingBoothDataAccess.Value;
    public IParkingBundleDataAccess ParkingBundles => lazyParkingBundleDataAccess.Value;
    public ISensorDataAccess Sensors => lazySensorDataAccess.Value;
    public ISensorActionOccupiedDataAccess SensorActionOccupieds => lazySensorActionOccupiedDataAccess.Value;
    public ISensorActionsRawDataAccess SensorActionsRaws => lazySensorActionsRawDataAccess.Value;
    public ISensorBatteryUpdateDataAccess SensorBatteryUpdates => lazySensorBatteryUpdateDataAccess.Value;
    public ISensorInstallDataAccess SensorInstalls => lazySensorInstallDataAccess.Value;
    public ISensorTypeDataAccess SensorTypes => lazySensorTypeDataAccess.Value;
    public ISensorUpdateDataAccess SensorUpdates => lazySensorUpdateDataAccess.Value;
    public IZipCodeDataAccess ZipCodes => lazyZipCodeDataAccess.Value;

    public PappDbUnitOfWork(PappDbContext context, ILogger<IUnitOfWork<PappDbContext>> logger) : base(context, logger)
    {
        lazyBoothDataAccess = new(() => new(context));
        lazyBundleDataAccess = new(() => new(context));
        lazyChargerDataAccess = new(() => new(context));
        lazyChargerConnectorDataAccess = new(() => new(context));
        lazyChargerTypeDataAccess = new(() => new(context));
        lazyCountryDataAccess = new(() => new(context));
        lazyImageDataAccess = new(() => new(context));
        lazyManufacturerDataAccess = new(() => new(context));
        lazyOperatorDataAccess = new(() => new(context));
        lazyParkingAreaDataAccess = new(() => new(context));
        lazyParkingAreaTransactionDataAccess = new(() => new(context));
        lazyParkingBoothDataAccess = new(() => new(context));
        lazyParkingBundleDataAccess = new(() => new(context));
        lazySensorDataAccess = new(() => new(context));
        lazySensorActionOccupiedDataAccess = new(() => new(context));
        lazySensorActionsRawDataAccess = new(() => new(context));
        lazySensorBatteryUpdateDataAccess = new(() => new(context));
        lazySensorInstallDataAccess = new(() => new(context));
        lazySensorTypeDataAccess = new(() => new(context));
        lazySensorUpdateDataAccess = new(() => new(context));
        lazyZipCodeDataAccess = new(() => new(context));
    }
}
