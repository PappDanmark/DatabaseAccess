using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Papp.Persistence.DataAccess;

public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private TContext DbContext { get; }
    private bool Disposed;
    private IDictionary<string, object> DataAccessObjects { get; }
    private IDbContextTransaction? Transaction;
    private ILogger<IUnitOfWork<TContext>> Logger { get; }

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
            this.Transaction = null;
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
            this.Transaction = null;
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
