using Papp.Domain;

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

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await base.GetFirstOrDefaultAsync(new Specification<Image>(e => e.Id.Equals(id)));
        return entity != null;
    }
}
