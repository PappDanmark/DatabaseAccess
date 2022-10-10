using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Papp.Persistence.DataAccess;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : PappDbContext
{
    private TContext DbContext { get; }
    private bool Disposed;
    // For future decoupling.
    private IDictionary<string, object> DataAccessObjects { get; }
    private IDbContextTransaction? Transaction;
    private ILogger<IUnitOfWork<TContext>> Logger { get; }

    // TODO: Decouple
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
    public IParkingAreaTransactionDataAccess ParkingAreaTransactions { get; private set; }
    public IParkingBoothDataAccess ParkingBooths { get; private set; }
    public ISensorDataAccess Sensors { get; private set; }
    public ISensorBatteryUpdateDataAccess SensorBatteryUpdates { get; private set; }
    public ISensorInstallDataAccess SensorInstalls { get; private set; }
    public ISensorTypeDataAccess SensorTypes { get; private set; }
    public ISensorUpdateDataAccess SensorUpdates { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logging service.</param>
    public UnitOfWork(TContext context, ILogger<IUnitOfWork<TContext>> logger)
    {
        this.DbContext = context;
        this.Disposed = false;
        this.DataAccessObjects = new Dictionary<string, object>();
        this.Transaction = null;
        this.Logger = logger;

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
        this.ParkingAreaTransactions = new ParkingAreaTransactionDataAccess(context);
        this.ParkingBooths = new ParkingBoothDataAccess(context);
        this.Sensors = new SensorDataAccess(context);
        this.SensorBatteryUpdates = new SensorBatteryUpdateDataAccess(context);
        this.SensorInstalls = new SensorInstallDataAccess(context);
        this.SensorTypes = new SensorTypeDataAccess(context);
        this.SensorUpdates = new SensorUpdateDataAccess(context);
    }

    /// <inheritdoc/>
    public void BeginTransaction()
    {
        if (this.Transaction == null)
        {
            this.Transaction = this.DbContext.Database.BeginTransaction();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing BeginTransaction().");
            this.Logger.LogError("[UnitOfWork]: The ongoing transaction hasn't yet been commited.");
        }
    }

    /// <inheritdoc/>
    public async Task BeginTransactionAsync()
    {
        if (this.Transaction == null)
        {
            this.Transaction = await this.DbContext.Database.BeginTransactionAsync();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing BeginTransactionAsync().");
            this.Logger.LogError("[UnitOfWork]: The ongoing transaction hasn't yet been commited.");
        }
    }

    /// <inheritdoc/>
    public void Commit()
    {
        if (this.Transaction != null)
        {
            this.Transaction.Commit();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing Commit().");
            this.Logger.LogError("[UnitOfWork]: No ongoing transaction has been started.");
        }
    }

    /// <inheritdoc/>
    public async Task CommitAsync()
    {
        if (this.Transaction != null)
        {
            await this.Transaction.CommitAsync();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing CommitAsync().");
            this.Logger.LogError("[UnitOfWork]: No ongoing transaction has been started.");
        }
    }

    /// <inheritdoc/>
    public void Rollback()
    {
        if (this.Transaction != null)
        {
            this.Transaction.Rollback();
            this.Transaction.Dispose();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing Rollback().");
            this.Logger.LogError("[UnitOfWork]: No ongoing transaction has been started.");
        }
    }

    /// <inheritdoc/>
    public async Task RollbackAsync()
    {
        if (this.Transaction != null)
        {
            await this.Transaction.RollbackAsync();
            await this.Transaction.DisposeAsync();
        }
        else
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing RollbackAsync().");
            this.Logger.LogError("[UnitOfWork]: No ongoing transaction has been started.");
        }
    }

    /// <inheritdoc/>
    public int SaveChanges()
    {
        try
        {
            return this.DbContext.SaveChanges();
        }
        catch (Exception e)
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing SaveChanges().");
            this.Logger.LogError("[UnitOfWork]: Error in {0}.", e);
            return -1;
        }
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await this.DbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            this.Logger.LogError("[UnitOfWork]: Error while executing SaveChanges().");
            this.Logger.LogError("[UnitOfWork]: Error in {0}.", e);
            return -1;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">Boolean indicating whether the call comes from a Dispose method (true) or a finalizer (false).</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.Disposed)
        {
            this.Disposed = true;
            if (disposing)
            {
                this.DataAccessObjects.Clear();
                this.DbContext.Dispose();
            }
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing">Boolean indicating whether the call comes from a Dispose method (true) or a finalizer (false).</param>
    protected async ValueTask DisposeAsync(bool disposing)
    {
        if (!this.Disposed)
        {
            this.Disposed = true;
            if (disposing)
            {
                this.DataAccessObjects.Clear();
                await this.DbContext.DisposeAsync();
            }
        }
    }
}
