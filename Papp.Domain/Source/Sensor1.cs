using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// Table that contains all sensors.
    /// </summary>
    public partial class Sensor1
    {
        public Sensor1()
        {
            SensorBatteryUpdates = new HashSet<SensorBatteryUpdate>();
            SensorInstalls = new HashSet<SensorInstall>();
            SensorUpdates = new HashSet<SensorUpdate>();
        }

        public string Id { get; set; }
        public Guid Type { get; set; }
        /// <summary>
        /// Latest occupied update. If null, then none has been recorded or DB error.
        /// </summary>
        public bool? LatestOccupied { get; set; }
        /// <summary>
        /// The time of the latest occupied status update. If null, none has occurred or DB error.
        /// </summary>
        public DateTime? LatestOccupiedTimestamp { get; set; }
        /// <summary>
        /// The latest battery update. If null, none has occurred or DB error.
        /// </summary>
        public short? LatestBattery { get; set; }
        /// <summary>
        /// The time of the latest battery update. If null, none has occurred or DB error.
        /// </summary>
        public DateTime? LatestBatteryTimestamp { get; set; }

        public virtual SensorType TypeNavigation { get; set; }
        public virtual ICollection<SensorBatteryUpdate> SensorBatteryUpdates { get; set; }
        public virtual ICollection<SensorInstall> SensorInstalls { get; set; }
        public virtual ICollection<SensorUpdate> SensorUpdates { get; set; }
    }
}
