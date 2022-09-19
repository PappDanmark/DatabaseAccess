namespace Papp.Domain;

/// <summary>
/// Table that contains all battery updates.
/// </summary>
public partial class SensorBatteryUpdate
{
    public Guid Id { get; set; }
    public DateTime Ts { get; set; }
    /// <summary>
    /// Percent battery left.
    /// </summary>
    public float Percent { get; set; }
    /// <summary>
    /// Reference to sensor table.
    /// </summary>
    public string SensorId { get; set; }

    public virtual Sensor Sensor { get; set; }
}
