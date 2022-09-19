namespace Papp.Domain;

public partial class SensorActionOccupied
{
    public SensorActionOccupied()
    {
        Sensor1s = new HashSet<Sensor1>();
    }

    public Guid OsaId { get; set; }
    public string SensorId { get; set; }
    public bool Occupied { get; set; }
    public DateTime ActionTimestamp { get; set; }

    public virtual Sensor1 Sensor { get; set; }
    public virtual ICollection<Sensor1> Sensor1s { get; set; }
}
