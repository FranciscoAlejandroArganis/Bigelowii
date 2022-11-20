/// <summary>
/// Efecto que daña a una unidad
/// </summary>
public class Damage : Effect
{

    /// <summary>
    /// La cantidad de daño que se aplica, después de las modificaciones
    /// </summary>
    public uint damage;

    /// <summary>
    /// Construye un nuevo efecto de daño
    /// </summary>
    /// <param name="damage">La cantidad de daño que se aplicará</param>
    public Damage (uint damage)
    {
        this.damage = damage;
    }

    public override void Apply(Unit unit)
    {
        if (unit.health <= damage)
            unit.health = 0;
        else
            unit.health -= damage;
    }

}
