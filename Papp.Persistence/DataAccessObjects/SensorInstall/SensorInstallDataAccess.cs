using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class SensorInstallDataAccess : GenericDataAccess<SensorInstall>, ISensorInstallDataAccess
{
    private readonly PappDbContext DbContext;

    public SensorInstallDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public SensorInstallDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
