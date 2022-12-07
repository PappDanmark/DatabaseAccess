using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class ParkingBooth
    {
        public ParkingBooth()
        {
            LegacySensors = new HashSet<LegacySensor>();
        }

        public Guid ParkingBoothId { get; set; }
        public short BoothNumber { get; set; }
        public Guid? PoiId { get; set; }

        public virtual ParkingBundle Poi { get; set; }
        public virtual ICollection<LegacySensor> LegacySensors { get; set; }
    }
}
