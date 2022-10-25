using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ImageDataAccess : GenericDataAccess<Image>, IImageDataAccess
{
    private readonly PappDbContext DbContext;

    public ImageDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ImageDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }
}
