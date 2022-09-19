namespace Papp.Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private PappDbContext context;
    public IBoothDataAccess boothDataAccess { get; private set; }

    public UnitOfWork(PappDbContext context)
    {
        this.context = context;
        this.boothDataAccess = new BoothDataAccess(context);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
