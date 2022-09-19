namespace Papp.Persistence.DataAccess;

public interface IUnitOfWork
{
    IBoothDataAccess boothDataAccess{ get; }

    Task SaveChangesAsync();
}
