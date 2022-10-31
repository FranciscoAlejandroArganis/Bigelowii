/// <summary>
/// Acción que requiere de una celda objetivo
/// </summary>
public abstract class CellTargetAction : TargetedAction
{

    /// <summary>
    /// La celda objetivo de la acción
    /// </summary>
    protected Cell targetCell;

    /// <summary>
    /// Construye una nueva acción con una celda objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public CellTargetAction(Unit unit) : base(unit) { }

    public override void SetTarget(Cell target)
    {
        targetCell = target;
    }

    public override Cell GetTarget()
    {
        return targetCell;
    }

}
