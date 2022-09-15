using NpgsqlTypes;

namespace Papp.Domain;

public partial class ParkingBundle
{
    public ParkingBundle()
    {
        ParkingBooths = new HashSet<ParkingBooth>();
    }

    public NpgsqlPoint Location { get; set; }
    public Guid PoiId { get; set; }
    public string Address { get; set; }
    public string PappPoiId { get; set; }
    public int Zip { get; set; }

    public virtual ZipCode ZipNavigation { get; set; }
    public virtual ICollection<ParkingBooth> ParkingBooths { get; set; }
}
