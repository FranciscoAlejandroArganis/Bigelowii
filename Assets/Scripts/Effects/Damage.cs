/// <summary>
/// Efecto que da�a a una unidad
/// </summary>
public class Damage : Effect
{

    /// <summary>
    /// La cantidad de da�o que se aplica, despu�s de las modificaciones
    /// </summary>
    public uint damage;

    /// <summary>
    /// Construye un nuevo efecto de da�o
    /// </summary>
    /// <param name="damage">La cantidad de da�o que se aplicar�</param>
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
