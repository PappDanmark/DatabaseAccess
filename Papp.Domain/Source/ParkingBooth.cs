using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class ParkingBooth
    {
        public ParkingBooth()
        {
            Sensor1s = new HashSet<Sensor1>();
        }

        public Guid ParkingBoothId { get; set; }
        public short BoothNumber { get; set; }
        public Guid? PoiId { get; set; }

        public virtual ParkingBundle Poi { get; set; }
        public virtual ICollection<Sensor1> Sensor1s { get; set; }
    }
}
