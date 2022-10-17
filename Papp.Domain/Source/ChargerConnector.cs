using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    /// <summary>
    /// Contains all connector types.
    /// </summary>
    public partial class ChargerConnector
    {
        public ChargerConnector()
        {
            ChargerTypes = new HashSet<ChargerType>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ChargerType> ChargerTypes { get; set; }
    }
}
