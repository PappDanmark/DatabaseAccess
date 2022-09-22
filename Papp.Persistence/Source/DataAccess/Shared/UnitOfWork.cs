namespace Papp.Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private PappDbContext context;
    public IBoothDataAccess BoothDataAccess { get; private set; }
    public IBundleDataAccess BundleDataAccess{ get; private set; }
    public IChargerDataAccess ChargerDataAccess{ get; private set; }
    public IChargerConnectorDataAccess ChargerConnectorDataAccess{ get; private set; }
    public IChargerTypeDataAccess ChargerTypeDataAccess{ get; private set; }
    public ICountryDataAccess CountryDataAccess{ get; private set; }
    public IImageDataAccess ImageDataAccess{ get; private set; }
    public IManufacturerDataAccess ManufacturerDataAccess{ get; private set; }
    public IOperatorDataAccess OperatorDataAccess{ get; private set; }
    public IParkingAreaDataAccess ParkingAreaDataAccess{ get; private set; }
    public IParkingAreaTransactionDataAccess ParkingAreaTransactionDataAccess{ get; private set; }
    public IParkingBoothDataAccess ParkingBoothDataAccess{ get; private set; }
    public ISensorDataAccess SensorDataAccess{ get; private set; }
    public ISensorBatteryUpdateDataAccess SensorBatteryUpdateDataAccess{ get; private set; }
    public ISensorInstallDataAccess SensorInstallDataAccess{ get; private set; }
    public ISensorTypeDataAccess SensorTypeDataAccess{ get; private set; }
    public ISensorUpdateDataAccess SensorUpdateDataAccess{ get; private set; }

    public UnitOfWork(PappDbContext context)
    {
        this.context = context;
        this.BoothDataAccess = new BoothDataAccess(context);
        this.BundleDataAccess = new BundleDataAccess(context);
        this.ChargerDataAccess = new ChargerDataAccess(context);
        this.ChargerConnectorDataAccess = new ChargerConnectorDataAccess(context);
        this.ChargerTypeDataAccess = new ChargerTypeDataAccess(context);
        this.CountryDataAccess = new CountryDataAccess(context);
        this.ImageDataAccess = new ImageDataAccess(context);
        this.ManufacturerDataAccess = new ManufacturerDataAccess(context);
        this.OperatorDataAccess = new OperatorDataAccess(context);
        this.ParkingAreaDataAccess = new ParkingAreaDataAccess(context);
        this.ParkingAreaTransactionDataAccess = new ParkingAreaTransactionDataAccess(context);
        this.ParkingBoothDataAccess = new ParkingBoothDataAccess(context);
        this.SensorDataAccess = new SensorDataAccess(context);
        this.SensorBatteryUpdateDataAccess = new SensorBatteryUpdateDataAccess(context);
        this.SensorInstallDataAccess = new SensorInstallDataAccess(context);
        this.SensorTypeDataAccess = new SensorTypeDataAccess(context);
        this.SensorUpdateDataAccess = new SensorUpdateDataAccess(context);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
