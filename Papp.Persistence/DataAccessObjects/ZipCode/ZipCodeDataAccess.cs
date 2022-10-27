using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ZipCodeDataAccess : GenericDataAccess<ZipCode>, IZipCodeDataAccess
{
    private readonly PappDbContext DbContext;

    public ZipCodeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ZipCodeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
