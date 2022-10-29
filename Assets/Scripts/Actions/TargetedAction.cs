/// <summary>
/// Acci�n que requiere de un objetivo
/// </summary>
public abstract class TargetedAction : Action
{

    /// <summary>
    /// La b�squeda que se usa para buscar objetivos
    /// </summary>
    protected Search search;

    /// <summary>
    /// Construye una nueva acci�n con objetivo
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    public TargetedAction(Unit unit) : base(unit) { }

    public override bool SearchTargets()
    {
        bool doable = false;
        search.FindCells(unit.cell);
        foreach (Cell cell in search.results)
        {
            if (ValidTarget(cell))
            {
                cell.highlight.Add(Highlight.State.Target);
                doable = true;
            }
        }
        return doable;
    }

    public override void ClearTargets()
    {
        foreach (Cell cell in search.results)
            cell.highlight.Remove(Highlight.State.Target);
        search.results.Clear();
    }

    /// <summary>
    /// Predicado que determina si la celda especificada es un objetivo v�lido de la acci�n
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda es un objetivo v�lido</returns>
    protected abstract bool ValidTarget(Cell cell);

}
