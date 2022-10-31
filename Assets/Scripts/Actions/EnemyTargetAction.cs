/// <summary>
/// Acción que requiere de una unidad enemiga como objetivo
/// </summary>
public abstract class EnemyTargetAction : UnitTargetAction
{

    /// <summary>
    /// Construye una nueva acción con una unidad enemiga como objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    protected EnemyTargetAction(Unit unit) : base(unit) { }

    protected override bool ValidTarget(Cell cell)
    {
        return cell.unit && unit.IsHostile(cell.unit);
    }

}
