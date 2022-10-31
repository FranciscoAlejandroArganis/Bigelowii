/// <summary>
/// Acción que requiere de una unidad objetivo
/// </summary>
public abstract class UnitTargetAction : TargetedAction
{

    /// <summary>
    /// La unidad objetivo de la acción
    /// </summary>
    protected Unit targetUnit;

    /// <summary>
    /// Construye una nueva acción con una unidad objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public UnitTargetAction(Unit unit) : base(unit) { }

    public override void SetTarget(Cell target)
    {
        targetUnit = target.unit;
    }

    public override Cell GetTarget()
    {
        return targetUnit.cell;
    }

}
