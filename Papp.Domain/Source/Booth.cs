using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// A parking booth.
    /// </summary>
    public partial class Booth
    {
        public Booth()
        {
            SensorInstalls = new HashSet<SensorInstall>();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// Which number this booth has in its bundle.
        /// </summary>
        public short BoothNumber { get; set; }
        /// <summary>
        /// The muncipality ID of this booth.
        /// </summary>
        public string MuncipalityId { get; set; }
        /// <summary>
        /// Following the OpeningHours standard. If null this is not a handicap booth.
        /// </summary>
        public string HandicapOh { get; set; }
        /// <summary>
        /// Following the OpeningHours standard. If null this is not electric exclusive.
        /// </summary>
        public string ElectricExclusiveOh { get; set; }
        /// <summary>
        /// Following the OpeningHours standard. If null this is not exclusive for craftsmen.
        /// </summary>
        public string CraftsmenExclusiveOh { get; set; }
        /// <summary>
        /// If null then this booth does not have an associated charger.
        /// </summary>
        public Guid? Charger { get; set; }
        /// <summary>
        /// Referencing the bundle this booth is part of.
        /// </summary>
        public int Bundle { get; set; }
        /// <summary>
        /// Referencing the sensor installed, and if null then there&apos;s no sensor installed.
        /// </summary>
        public int? SensorInstall { get; set; }

        public virtual Bundle BundleNavigation { get; set; }
        public virtual Charger ChargerNavigation { get; set; }
        public virtual SensorInstall SensorInstallNavigation { get; set; }
        public virtual ICollection<SensorInstall> SensorInstalls { get; set; }
    }
}
