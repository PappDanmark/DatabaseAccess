namespace Papp.Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private PappDbContext context;
    public IBoothDataAccess Booths { get; private set; }
    public IBundleDataAccess Bundles { get; private set; }
    public IChargerDataAccess Chargers { get; private set; }
    public IChargerConnectorDataAccess ChargerConnectors { get; private set; }
    public IChargerTypeDataAccess ChargerTypes { get; private set; }
    public ICountryDataAccess Countries { get; private set; }
    public IImageDataAccess Images { get; private set; }
    public IManufacturerDataAccess Manufacturers { get; private set; }
    public IOperatorDataAccess Operators { get; private set; }
    public IParkingAreaDataAccess ParkingAreas { get; private set; }
    public IParkingAreaOccupancyDataAccess ParkingAreaOccupancies { get; private set; }
    public IParkingAreaTransactionDataAccess ParkingAreaTransactions { get; private set; }
    public IParkingBoothDataAccess ParkingBooths { get; private set; }
    public ISensorDataAccess Sensors { get; private set; }
    public ISensorBatteryUpdateDataAccess SensorBatteryUpdates { get; private set; }
    public ISensorInstallDataAccess SensorInstalls { get; private set; }
    public ISensorTypeDataAccess SensorTypes { get; private set; }
    public ISensorUpdateDataAccess SensorUpdates { get; private set; }

    public UnitOfWork(PappDbContext context)
    {
        this.context = context;
        this.Booths = new BoothDataAccess(context);
        this.Bundles = new BundleDataAccess(context);
        this.Chargers = new ChargerDataAccess(context);
        this.ChargerConnectors = new ChargerConnectorDataAccess(context);
        this.ChargerTypes = new ChargerTypeDataAccess(context);
        this.Countries = new CountryDataAccess(context);
        this.Images = new ImageDataAccess(context);
        this.Manufacturers = new ManufacturerDataAccess(context);
        this.Operators = new OperatorDataAccess(context);
        this.ParkingAreas = new ParkingAreaDataAccess(context);
        this.ParkingAreaOccupancies = new ParkingAreaOccupancyDataAccess(context);
        this.ParkingAreaTransactions = new ParkingAreaTransactionDataAccess(context);
        this.ParkingBooths = new ParkingBoothDataAccess(context);
        this.Sensors = new SensorDataAccess(context);
        this.SensorBatteryUpdates = new SensorBatteryUpdateDataAccess(context);
        this.SensorInstalls = new SensorInstallDataAccess(context);
        this.SensorTypes = new SensorTypeDataAccess(context);
        this.SensorUpdates = new SensorUpdateDataAccess(context);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
