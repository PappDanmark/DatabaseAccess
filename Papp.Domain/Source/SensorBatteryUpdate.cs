﻿using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorBatteryUpdate
    {
        public Guid Id { get; set; }
        public string SensorId { get; set; }
        public DateTime Ts { get; set; }
        public short Battery { get; set; }
        public DateTime TsServer { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
