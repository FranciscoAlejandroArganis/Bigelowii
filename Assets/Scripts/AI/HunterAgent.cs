/// <summary>
/// Agente inteligente que da caza a una unidad presa
/// <para>La acción distinguida es atacar, desde una celda adyacente, a la presa</para>
/// <para>Mientras el agente pueda alcanzar a la presa, ignorará cualquier otra unidad hasta que la presa sea destruida o se vuelva inalcanzable</para>
/// <para>Después termina su turno</para>
/// </summary>
public abstract class HunterAgent : BasicAgent
{

    /// <summary>
    /// Construye un nuevo agente cazador
    /// </summary>
    /// <param name="unit">La unidad que controlará el agente</param>
    public HunterAgent(Unit unit) : base(unit, 1) { }

    /// <summary>
    /// Regresa <c>true</c> cuando la celda es adyacente a la presa
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda es adyacente a la celda de la presa</returns>
    protected bool AdjacentToPrey(Cell cell)
    {
        return cell.EdgeIndex(targetUnit.cell) < 6;
    }

    protected override void SearchDestination()
    {
        if (targetUnit)
        {
            FirstMatchSearch search = new FirstMatchSearch(AdjacentToPrey);
            search.FindCells(unit.cell);
            if (search.firstMatch)
            {
                destination = search.firstMatch;
                BestDestination();
            }
            else
                targetUnit = null;
            ClearCells(search);
        }
        if (!destination)
            SearchNewPrey();
    }

    protected override bool ActionCanBeUsed()
    {
        return AdjacentToPrey(unit.cell);
    }

    /// <summary>
    /// Se manda a llamar cuando el agente busca un destino pero no tiene actualmente una presa
    /// <para>Busca una nueva presa</para>
    /// <para>Si la encuentra, asigna el destino a la celda más cercana a su presa que puede alcanzar</para>
    /// <para>Si no puede encontrar una nueva presa, deja el destino en <c>null</c></para>
    /// </summary>
    protected abstract void SearchNewPrey();

}
