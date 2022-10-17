﻿using System;
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

        public virtual SensorType TypeNavigation { get; set; }
        public virtual ICollection<SensorBatteryUpdate> SensorBatteryUpdates { get; set; }
        public virtual ICollection<SensorInstall> SensorInstalls { get; set; }
        public virtual ICollection<SensorUpdate> SensorUpdates { get; set; }
    }
}
