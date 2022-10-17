using System;
using System.Collections.Generic;

namespace Papp.Domain
{
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
        /// e.g. &apos;zip&apos;, &apos;rar&apos;, &apos;gzip&apos; etc. If null then the bytea is not compressed.
        /// </summary>
        public string CompressionType { get; set; }
        /// <summary>
        /// e.g. &apos;image/jpeg&apos;, &apos;image/png&apos; etc.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// The raw data associated with this image.
        /// </summary>
        public byte[] Data { get; set; }

        public virtual ICollection<SensorInstall> SensorInstalls { get; set; }
    }
}
