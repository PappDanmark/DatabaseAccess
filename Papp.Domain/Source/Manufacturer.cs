using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// This table contains all the sensor producing companies.
    /// </summary>
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            SensorTypes = new HashSet<SensorType>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SensorType> SensorTypes { get; set; }
    }
}
