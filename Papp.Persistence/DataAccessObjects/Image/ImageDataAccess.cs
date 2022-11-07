using Microsoft.EntityFrameworkCore;
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

    /// <inheritdoc/>
    public Image? Update(int id, Image image)
    {
        var existing = DbContext.Images.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = image.Name;
        existing.CompressionType = image.CompressionType;
        existing.MimeType = image.MimeType;
        existing.Data = image.Data;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Image?> UpdateAsync(int id, Image image)
    {
        var existing = await DbContext.Images.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = image.Name;
        existing.CompressionType = image.CompressionType;
        existing.MimeType = image.MimeType;
        existing.Data = image.Data;

        return existing;
    }
}
