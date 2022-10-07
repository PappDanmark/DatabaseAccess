using System;
using System.Collections.Generic;

namespace Papp.Domain
{
    public partial class Country
    {
        public Country()
        {
            ZipCodes = new HashSet<ZipCode>();
        }

        public short Iso3166Numeric { get; set; }
        public string CommonName { get; set; }
        public string OfficialName { get; set; }
        public string Iso3166Alpha2 { get; set; }
        public string Iso3166Alpha3 { get; set; }
        public int Population { get; set; }
        public int AreaKm2 { get; set; }

        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}
