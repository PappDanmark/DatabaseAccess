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
            ParkingAreaTransaction0s = new HashSet<ParkingAreaTransaction0>();
            ParkingAreaTransaction1s = new HashSet<ParkingAreaTransaction1>();
            ParkingAreaTransaction2s = new HashSet<ParkingAreaTransaction2>();
            ParkingAreaTransaction3s = new HashSet<ParkingAreaTransaction3>();
            ParkingAreaTransaction4s = new HashSet<ParkingAreaTransaction4>();
            ParkingAreaTransaction5s = new HashSet<ParkingAreaTransaction5>();
            ParkingAreaTransaction6s = new HashSet<ParkingAreaTransaction6>();
            ParkingAreaTransaction7s = new HashSet<ParkingAreaTransaction7>();
            ParkingAreaTransaction8s = new HashSet<ParkingAreaTransaction8>();
            ParkingAreaTransaction9s = new HashSet<ParkingAreaTransaction9>();
            ParkingAreaTransactionOlds = new HashSet<ParkingAreaTransactionOld>();
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
        public virtual ICollection<ParkingAreaTransaction0> ParkingAreaTransaction0s { get; set; }
        public virtual ICollection<ParkingAreaTransaction1> ParkingAreaTransaction1s { get; set; }
        public virtual ICollection<ParkingAreaTransaction2> ParkingAreaTransaction2s { get; set; }
        public virtual ICollection<ParkingAreaTransaction3> ParkingAreaTransaction3s { get; set; }
        public virtual ICollection<ParkingAreaTransaction4> ParkingAreaTransaction4s { get; set; }
        public virtual ICollection<ParkingAreaTransaction5> ParkingAreaTransaction5s { get; set; }
        public virtual ICollection<ParkingAreaTransaction6> ParkingAreaTransaction6s { get; set; }
        public virtual ICollection<ParkingAreaTransaction7> ParkingAreaTransaction7s { get; set; }
        public virtual ICollection<ParkingAreaTransaction8> ParkingAreaTransaction8s { get; set; }
        public virtual ICollection<ParkingAreaTransaction9> ParkingAreaTransaction9s { get; set; }
        public virtual ICollection<ParkingAreaTransactionOld> ParkingAreaTransactionOlds { get; set; }
    }
}
