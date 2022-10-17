using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class Charger
    {
        public Charger()
        {
            Booths = new HashSet<Booth>();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// References the charger type.
        /// </summary>
        public int ChargerType { get; set; }
        /// <summary>
        /// The operators ID of this charging station.
        /// </summary>
        public string OperatorId { get; set; }

        public virtual ChargerType ChargerTypeNavigation { get; set; }
        public virtual ICollection<Booth> Booths { get; set; }
    }
}
