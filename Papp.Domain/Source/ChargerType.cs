namespace Papp.Domain;

public partial class ChargerType
{
    public ChargerType()
    {
        Chargers = new HashSet<Charger>();
    }

    public int Id { get; set; }
    public short Operator { get; set; }
    /// <summary>
    /// The charger capacity.
    /// </summary>
    public short Kilowatt { get; set; }
    /// <summary>
    /// Whether the charger is DC or AC.
    /// </summary>
    public bool Dc { get; set; }
    /// <summary>
    /// The name/model of the charger type.
    /// </summary>
    public string Name { get; set; }
    public short Connector { get; set; }

    public virtual ChargerConnector ConnectorNavigation { get; set; }
    public virtual Operator OperatorNavigation { get; set; }
    public virtual ICollection<Charger> Chargers { get; set; }
}
