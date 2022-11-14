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

    /// <inheritdoc/>
    private protected override void UpdateEntityFields(Image src, Image dst)
    {
        dst.Name = src.Name;
        dst.CompressionType = src.CompressionType;
        dst.MimeType = src.MimeType;
        dst.Data = src.Data;
    }
}
