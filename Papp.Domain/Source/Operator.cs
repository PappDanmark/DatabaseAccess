namespace Papp.Domain;

/// <summary>
/// The charger operator.
/// </summary>
public partial class Operator
{
    public Operator()
    {
        ChargerTypes = new HashSet<ChargerType>();
    }

    public short Id { get; set; }
    /// <summary>
    /// Name of the charger operator.
    /// </summary>
    public string Name { get; set; }

    public virtual ICollection<ChargerType> ChargerTypes { get; set; }
}
