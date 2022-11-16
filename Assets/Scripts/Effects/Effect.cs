/// <summary>
/// Efecto de una acción sobre una unidad
/// </summary>
public abstract class Effect
{

    /// <summary>
    /// Modifica el efecto por todos los comportamientos de la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad de la cual sus comportamientos modifican el efecto</param>
    public void BehaviorModifiers(Unit unit)
    {
        foreach (Behavior behavior in unit.behaviors)
            behavior.Modify(this);
    }

    /// <summary>
    /// Aplica el efecto a la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad a la que se le aplica el efecto</param>
    public abstract void Apply(Unit unit);

}
