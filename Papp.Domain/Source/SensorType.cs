using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class SensorType
    {
        public SensorType()
        {
            ParkingAreas = new HashSet<ParkingArea>();
            Sensor1s = new HashSet<Sensor1>();
            Sensors = new HashSet<Sensor>();
        }

        public Guid Id { get; set; }
        public string Model { get; set; }
        public short Manufacturer { get; set; }

        public virtual Manufacturer ManufacturerNavigation { get; set; }
        public virtual ICollection<ParkingArea> ParkingAreas { get; set; }
        public virtual ICollection<Sensor1> Sensor1s { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
