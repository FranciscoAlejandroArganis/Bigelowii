using System.Collections.Generic;

/// <summary>
/// Búsqueda de celdas usando un recorrido BFS
/// </summary>
public abstract class BreadthFirstSearch : Search
{

    /// <summary>
    /// La cola auxiliar para el recorrido BFS
    /// </summary>
    private Queue<Cell> queue;

    /// <summary>
    /// Construye una nueva búsqueda con recorrido BFS
    /// </summary>
    public BreadthFirstSearch() : base()
    {
        queue = new Queue<Cell>();
    }

    public override void FindCells(Cell startingCell)
    {
        startingCell.visited = true;
        queue.Enqueue(startingCell);
        while (queue.Count > 0)
        {
            Cell currentCell = queue.Dequeue();
            results.Add(currentCell);
            foreach (Cell nextCell in currentCell.neighbors)
            {
                if (nextCell && !nextCell.visited && Edge(currentCell, nextCell))
                {
                    nextCell.visited = true;
                    queue.Enqueue(nextCell);
                }
            }
        }
    }

    /// <summary>
    /// Regresa <c>true</c> si la búsqueda debe continuar explorando por la arista que conecta las celdas especificados
    /// </summary>
    /// <param name="currentCell">La celda actual que se ha encontrado en la búsqueda</param>
    /// <param name="nextCell">Una celda no visitada adyacente a <c>currentCell</c></param>
    /// <returns><c>true</c> si <c>nextCell</c> debe entrar en la cola</returns>
    protected abstract bool Edge(Cell currentCell, Cell nextCell);

}
