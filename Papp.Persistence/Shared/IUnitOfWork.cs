using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Defines the interface for any UnitOfWork implementations.
/// </summary>
public interface IUnitOfWork<out TContext> : IDataAccessFactory, IDisposable, IAsyncDisposable where TContext : DbContext
{
    /// <summary>
    /// Gets the database context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    TContext DbContext { get; }

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
}
