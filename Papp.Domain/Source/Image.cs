namespace Papp.Domain;

/// <summary>
/// Single table for images.
/// </summary>
public partial class Image
{
    public Image()
    {
        SensorInstalls = new HashSet<SensorInstall>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// e.g. "zip", "rar", "gzip" etc. If null then the bytea is not compressed.
    /// </summary>
    public string CompressionType { get; set; }
    /// <summary>
    /// e.g. "image/jpeg", "image/png" etc.
    /// </summary>
    public string MimeType { get; set; }
    /// <summary>
    /// The raw data associated with this image.
    /// </summary>
    public byte[] Data { get; set; }

    public virtual ICollection<SensorInstall> SensorInstalls { get; set; }
}
