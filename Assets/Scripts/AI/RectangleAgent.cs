/// <summary>
/// Guión del agente inteligente que controla un rectángulo
/// <para>Toma como presa a la unidad enemiga con la menor salud</para>
/// </summary>
public class RectangleAgent : HunterAgent
{

    /// <summary>
    /// Construye un nuevo agente de un rectángulo
    /// </summary>
    /// <param name="rectangle">El rectángulo que contralará el agente</param>
    public RectangleAgent(Rectangle rectangle) : base(rectangle) { }

    protected override void SearchNewPrey()
    {
        MinimumSearch search = new MinimumSearch(AdjacentEnemyUnitHealth);
        search.FindCells(unit.cell);
        if (search.value < uint.MaxValue)
        {
            destination = search.minimum;
            foreach (Cell neighbor in destination.neighbors)
            {
                if (neighbor)
                {
                    Unit unit = neighbor.unit;
                    if (unit && unit.IsHostile(this.unit) && unit.health == search.value)
                    {
                        targetUnit = unit;
                        break;
                    }
                }
            }
            BestDestination();
        }
        ClearCells(search);
    }

    /// <summary>
    /// Regresa la mínima salud que tiene una unidad enemiga en una celda adyacente
    /// </summary>
    /// <param name="cell">La celda que se evalúa</param>
    /// <returns>La mínima salud de una unidad enemiga adyacente o <c>uint.MaxValue</c> si no hay unidades enemigas adyacentes</returns>
    private uint AdjacentEnemyUnitHealth(Cell cell)
    {
        uint minHealth = uint.MaxValue;
        foreach (Cell neighbor in cell.neighbors)
        {
            if (neighbor && neighbor.unit && neighbor.unit.IsHostile(unit))
            {
                uint unitHealth = neighbor.unit.health;
                if (unitHealth < minHealth)
                    minHealth = unitHealth;
            }
        }
        return minHealth;
    }

}
