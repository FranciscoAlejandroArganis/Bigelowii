/// <summary>
/// Búsqueda de vecindad
/// <para>Encuentra todas las celdas adyacentes a la celda de origen</para>
/// </summary>
public class NeighbourhoodSearch : Search
{

    public override void FindCells(Cell startingCell)
    {
        foreach (Cell currentCell in startingCell.neighbors)
            if (currentCell)
                results.Add(currentCell);
    }

}
