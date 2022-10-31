/// <summary>
/// Acci�n que requiere de una unidad enemiga como objetivo
/// </summary>
public abstract class EnemyTargetAction : UnitTargetAction
{

    /// <summary>
    /// Construye una nueva acci�n con una unidad enemiga como objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    protected EnemyTargetAction(Unit unit) : base(unit) { }

    protected override bool ValidTarget(Cell cell)
    {
        return cell.unit && unit.IsHostile(cell.unit);
    }

}
