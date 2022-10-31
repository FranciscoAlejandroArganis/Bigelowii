/// <summary>
/// Efecto de una acción sobre una unidad
/// </summary>
public abstract class Effect
{

    /// <summary>
    /// Aplica el efecto a la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad a la que se le aplica el efecto</param>
    public abstract void Apply(Unit unit);

}
