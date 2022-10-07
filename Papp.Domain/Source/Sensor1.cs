using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class Sensor1
    {
        public Sensor1()
        {
            SensorActionOccupieds = new HashSet<SensorActionOccupied>();
        }

        public string SensorId { get; set; }
        public DateTime InstallationTimestamp { get; set; }
        public short? Battery { get; set; }
        public bool? Occupied { get; set; }
        public Guid? LastUpdatedBySensorAction { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
        public Guid InstalledAtParkingBoothId { get; set; }
        public int InstallationDateTimeEpoch { get; set; }
        public Guid SensorTypeId { get; set; }

        public virtual ParkingBooth InstalledAtParkingBooth { get; set; }
        public virtual SensorActionOccupied LastUpdatedBySensorActionNavigation { get; set; }
        public virtual SensorType SensorType { get; set; }
        public virtual ICollection<SensorActionOccupied> SensorActionOccupieds { get; set; }
    }
}
