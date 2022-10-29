/// <summary>
/// Acción que no requiere de un objetivo
/// <para>Se confirma con la celda de la unidad</para>
/// </summary>
public abstract class UntargetedAction : Action
{

    /// <summary>
    /// Construye una nueva acción sin objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public UntargetedAction(Unit unit) : base(unit) { }

    public override void SetTarget(Cell target) { }

    public override Cell GetTarget()
    {
        return unit.cell;
    }

    public override bool SearchTargets()
    {
        unit.cell.highlight.Add(Highlight.State.Target);
        return true;
    }

    public override void ClearTargets()
    {
        unit.cell.highlight.Remove(Highlight.State.Target);
    }

}
