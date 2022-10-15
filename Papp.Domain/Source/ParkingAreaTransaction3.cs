using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class ParkingAreaTransaction3
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int ParkingAreaId { get; set; }
        public short OccupiedSpaces { get; set; }

        public virtual ParkingArea ParkingArea { get; set; }
    }
}
