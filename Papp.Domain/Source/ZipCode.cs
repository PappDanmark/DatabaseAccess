using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// Table that contains all zip codes and names
    /// </summary>
    public partial class ZipCode
    {
        public ZipCode()
        {
            Bundles = new HashSet<Bundle>();
            ParkingAreas = new HashSet<ParkingArea>();
            ParkingBundles = new HashSet<ParkingBundle>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Bundle> Bundles { get; set; }
        public virtual ICollection<ParkingArea> ParkingAreas { get; set; }
        public virtual ICollection<ParkingBundle> ParkingBundles { get; set; }
    }
}
