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

    protected override void SearchNewPrey()
    {
        FirstMatchSearch search = new FirstMatchSearch(AdjacentToEnemyUnit);
        search.FindCells(unit.cell);
        if (search.firstMatch)
        {
            destination = search.firstMatch;
            BestDestination();
        }
        ClearCells(search);
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
                targetUnit = neighbor.unit;
                return true;
            }
        }
        return false;
    }

}
