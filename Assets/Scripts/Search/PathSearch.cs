/// <summary>
/// Búsqueda de caminos
/// </summary>
public class PathSearch : BreadthFirstSearch
{

    /// <summary>
    /// Construye un camino hasta la celda destino especificada
    /// <para>Se asume que se ha realizado previamente un recorrido BFS y que la celda es alcanzable</para>
    /// <para>El camino empieza en la celda incial del recorrido y termina en <c>cell</c></para>
    /// </summary>
    /// <param name="cell">La celda destino</param>
    /// <returns>Un arreglo con el camino desde hasta <c>cell</c></returns>
    public static Cell[] BuildPathTo(Cell cell)
    {
        Cell[] path = new Cell[cell.distance + 1];
        uint index = cell.distance;
        while (true)
        {
            path[index] = cell;
            cell = cell.predecesor;
            if (!cell) break;
            index--;
        }
        return path;
    }

    /// <summary>
    /// Distancia máxima a partir de la celda inicial que se explorará durante el recorrido BFS
    /// </summary>
    private uint maxDistance;

    /// <summary>
    /// Construye una nueva búsqueda de caminos
    /// </summary>
    /// <param name="maxDistance">La longitud máxima que puede tener cualquier camino encontrado</param>
    public PathSearch(uint maxDistance) : base()
    {
        this.maxDistance = maxDistance;
    }

    protected override bool Edge(Cell currentCell, Cell nextCell)
    {
        if (nextCell.IsFree() && currentCell.distance < maxDistance)
        {
            nextCell.distance = currentCell.distance + 1;
            nextCell.predecesor = currentCell;
            return true;
        }
        return false;
    }

}
