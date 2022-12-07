using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorType
    {
        public SensorType()
        {
            ParkingAreas = new HashSet<ParkingArea>();
            Sensors = new HashSet<Sensor>();
            LegacySensors = new HashSet<LegacySensor>();
        }

        public Guid Id { get; set; }
        public string Model { get; set; }
        public short Manufacturer { get; set; }

        public virtual Manufacturer ManufacturerNavigation { get; set; }
        public virtual ICollection<ParkingArea> ParkingAreas { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
        public virtual ICollection<LegacySensor> LegacySensors { get; set; }
    }
}
