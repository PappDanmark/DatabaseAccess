using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorUpdate
    {
        public Guid Id { get; set; }
        public string SensorId { get; set; }
        public DateTime Ts { get; set; }
        public bool Occupied { get; set; }

        public virtual Sensor1 Sensor { get; set; }
    }
}
