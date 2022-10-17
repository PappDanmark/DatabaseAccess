using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorActionOccupied
    {
        public SensorActionOccupied()
        {
            Sensors = new HashSet<Sensor>();
        }

        public Guid OsaId { get; set; }
        public string SensorId { get; set; }
        public bool Occupied { get; set; }
        public DateTime ActionTimestamp { get; set; }

        public virtual Sensor Sensor { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
