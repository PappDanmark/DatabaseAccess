using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Papp.Domain
{
    /// <summary>
    /// Table that contains parking areas like parking houses, which aren&apos;t for a specific kind of car or person.
    /// </summary>
    public partial class ParkingArea
    {
        public ParkingArea()
        {
            ParkingAreaTransactions = new HashSet<ParkingAreaTransaction>();
        }

        public int Id { get; set; }
        public short? TotalSpaces { get; set; }
        /// <summary>
        /// The coordinate set for the placement of the ParkingArea on a map.
        /// </summary>
        public NpgsqlPoint Coordinates { get; set; }
        public int ZipCodeId { get; set; }
        public Guid? SensorTypeId { get; set; }
        public string PappId { get; set; }
        /// <summary>
        /// The coordinate set for the entry to the parking area.
        /// </summary>
        public NpgsqlPoint CoordinatesEntry { get; set; }
        /// <summary>
        /// The street name and number.
        /// </summary>
        public string Street { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The number of occupied spaces from the latest update. Null if it hasnt been updated or error in the DB.
        /// </summary>
        public short? LatestOccupiedSpaces { get; set; }
        /// <summary>
        /// The timestamp of the latest occupied_spaces update. Null if none or error in DB.
        /// </summary>
        public DateTime? LatestOccupiedTimestamp { get; set; }

        public virtual SensorType SensorType { get; set; }
        public virtual ZipCode ZipCode { get; set; }
        public virtual ICollection<ParkingAreaTransaction> ParkingAreaTransactions { get; set; }
    }
}
