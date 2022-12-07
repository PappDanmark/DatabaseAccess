using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorActionOccupied
    {
        public SensorActionOccupied()
        {
            LegacySensors = new HashSet<LegacySensor>();
        }

        public Guid OsaId { get; set; }
        public string SensorId { get; set; }
        public bool Occupied { get; set; }
        public DateTime ActionTimestamp { get; set; }

        public virtual LegacySensor LegacySensor { get; set; }
        public virtual ICollection<LegacySensor> LegacySensors { get; set; }
    }
}
