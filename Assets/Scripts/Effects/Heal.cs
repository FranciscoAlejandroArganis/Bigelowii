/// <summary>
/// Efecto que recupera la salud de una unidad
/// </summary>
public class Heal : Effect
{

    /// <summary>
    /// La cantidad de salud que se recupera
    /// </summary>
    private uint heal;

    /// <summary>
    /// Construye un nuevo efecto de recuperar salud
    /// </summary>
    /// <param name="heal">La cantidad de salud que se regenerará</param>
    public Heal(uint heal)
    {
        this.heal = heal;
    }

    public override void Apply(Unit unit)
    {
        unit.health += heal;
        if (unit.health > unit.maxHealth)
            unit.health = unit.maxHealth;
    }

}
