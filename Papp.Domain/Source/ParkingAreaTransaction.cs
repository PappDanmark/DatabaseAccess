namespace Papp.Domain;

public partial class ParkingAreaTransaction
{
    public Guid Id { get; set; }
    /// <summary>
    /// The timestamp of this transaction saved as UTC. It's important to set the time to UTC before inserting!
    /// </summary>
    public DateTime Timestamp { get; set; }
    public int ParkingAreaId { get; set; }
    public short OccupiedSpaces { get; set; }

    public virtual ParkingArea ParkingArea { get; set; }
}
