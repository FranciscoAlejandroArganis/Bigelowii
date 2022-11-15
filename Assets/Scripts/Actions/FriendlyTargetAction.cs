/// <summary>
/// Acción que requiere de una unidad aliada como objetivo
/// </summary>
public abstract class FriendlyTargetAction : UnitTargetAction
{

    /// <summary>
    /// Construye una nueva acción con una unidad aliada como objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public FriendlyTargetAction(Unit unit) : base(unit) { }

    protected override bool ValidTarget(Cell cell)
    {
        return cell.unit && unit.IsFriendly(cell.unit);
    }

}
