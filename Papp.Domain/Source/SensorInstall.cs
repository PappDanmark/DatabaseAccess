using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// Table with all sensor installations. Here one physical sensor could be installed more than once, but not at the same time.
    /// </summary>
    public partial class SensorInstall
    {
        public SensorInstall()
        {
            Booths = new HashSet<Booth>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Time of installation.
        /// </summary>
        public DateTime InstallTs { get; set; }
        /// <summary>
        /// If null, then the sensor is still active.
        /// </summary>
        public DateTime? UninstallTs { get; set; }
        public int InstallImage { get; set; }
        public Guid Booth { get; set; }
        /// <summary>
        /// The physical sensor installed in this entry.
        /// </summary>
        public string SensorId { get; set; }

        public virtual Booth BoothNavigation { get; set; }
        public virtual Image InstallImageNavigation { get; set; }
        public virtual Sensor Sensor { get; set; }
        public virtual ICollection<Booth> Booths { get; set; }
    }
}
