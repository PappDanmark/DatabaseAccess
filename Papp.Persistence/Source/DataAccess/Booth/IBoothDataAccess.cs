using Papp.Domain;

namespace Papp.Persistence.DataAccess;

public interface IBoothDataAccess : IGenericDataAccess<Booth>
{
    Task<bool> Exists(Guid id);
}
