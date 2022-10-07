using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class ParkingAreaOccupancy
    {
        public int? ParkingAreaId { get; set; }
        public DateTime? Timestamp { get; set; }
        public short? OccupiedSpaces { get; set; }
    }
}
