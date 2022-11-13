/// <summary>
/// Guión del agente inteligente que controla un triángulo
/// <para>Toma como presa a la unidad enemiga más cercana</para>
/// </summary>
public class TriangleAgent : HunterAgent
{

    /// <summary>
    /// Construye un nuevo agente de un triángulo
    /// </summary>
    /// <param name="triangle">El triángulo que controlará el agente</param>
    public TriangleAgent(Triangle triangle) : base(triangle) { }

    /// <summary>
    /// Busca un destino y asigna <c>destination</c> a esa celda
    /// <para><c>destination</c> es <c>null</c> si no se encunentra una celda destino para el turno actual</para>
    /// </summary>
    protected override void SearchDestination()
    {
        FirstMatchSearch search;
        if (prey)
        {
            search = new FirstMatchSearch(AdjacentToPrey);
            search.FindCells(unit.cell);
            if (search.firstMatch)
            {
                destination = search.firstMatch;
                BestDestination();
            }
            else
                prey = null;
            ClearCells(search);
        }
        if (!destination)
        {
            search = new FirstMatchSearch(AdjacentToEnemyUnit);
            search.FindCells(unit.cell);
            if (search.firstMatch)
            {
                destination = search.firstMatch;
                BestDestination();
            }
            ClearCells(search);
        }
    }

    /// <summary>
    /// Regresa <c>true</c> cuando la celda es adyacente a una unidad enemiga
    /// <para>Asigna la unidad como la nueva presa</para>
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda es adyacente a la celda de una unidad enemiga</returns>
    private bool AdjacentToEnemyUnit(Cell cell)
    {
        foreach (Cell neighbor in cell.neighbors)
        {
            if (neighbor && neighbor.unit && neighbor.unit.IsHostile(unit))
            {
                prey = neighbor.unit;
                return true;
            }
        }
        return false;
    }

}
