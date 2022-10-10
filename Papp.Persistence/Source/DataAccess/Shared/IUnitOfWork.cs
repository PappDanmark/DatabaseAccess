using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

public interface IUnitOfWork<TContext> : IDisposable, IAsyncDisposable where TContext : DbContext
{
    /// <summary>
    /// Starts a database transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Starts a database transaction asynchronously.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the database transaction.
    /// </summary>
    void Commit();

    /// <summary>
    /// Commits the database transaction asynchronously.
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    void Rollback();

    /// <summary>
    /// Discards all changes made to the database in the current transaction asynchronously.
    /// </summary>
    Task RollbackAsync();

    /// <summary>
    /// Saves all changes made in UnitOfWork's context to the database. 
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of state entries written to database or -1 if an error occured.</returns>
    int SaveChanges();

    /// <summary>
    /// Asynchronously saves all changes made in UnitOfWork's context to the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> containing the number of state entries written to database or -1 if an error occured.</returns>
    Task<int> SaveChangesAsync();

    // TODO: Decouple UnitOfWork from the specific data access objects
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
    ISensorDataAccess Sensors { get; }
    ISensorBatteryUpdateDataAccess SensorBatteryUpdates { get; }
    ISensorInstallDataAccess SensorInstalls { get; }
    ISensorTypeDataAccess SensorTypes { get; }
    ISensorUpdateDataAccess SensorUpdates { get; }
}
