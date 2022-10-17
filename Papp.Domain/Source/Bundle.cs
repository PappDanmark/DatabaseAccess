using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Papp.Domain
{
    public partial class Bundle
    {
        public Bundle()
        {
            Booths = new HashSet<Booth>();
        }

        public int Id { get; set; }
        public NpgsqlPoint Location { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }

        public virtual ZipCode ZipNavigation { get; set; }
        public virtual ICollection<Booth> Booths { get; set; }
    }
}
