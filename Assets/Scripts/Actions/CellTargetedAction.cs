/// <summary>
/// Acci�n que requiere de una celda objetivo
/// </summary>
public abstract class CellTargetedAction : TargetedAction
{

    /// <summary>
    /// La celda objetivo de la acci�n
    /// </summary>
    protected Cell targetCell;

    /// <summary>
    /// Construye una nueva acci�n con una celda objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    public CellTargetedAction(Unit unit) : base(unit) { }

    public override void SetTarget(Cell target)
    {
        targetCell = target;
    }

    public override Cell GetTarget()
    {
        return targetCell;
    }

}
