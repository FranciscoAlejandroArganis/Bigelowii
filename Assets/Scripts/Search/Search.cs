using System.Collections.Generic;

/// <summary>
/// Búsqueda de celdas
/// </summary>
public abstract class Search
{

    /// <summary>
    /// Lista con los resultados de la búsqueda
    /// </summary>
    public List<Cell> results;

    /// <summary>
    /// Construye una nueva búsqueda
    /// </summary>
    public Search()
    {
        results = new List<Cell>();
    }

    /// <summary>
    /// Busca celdas a partir de la celda inicial especificada y guarda los resultados en <c>results</c>
    /// </summary>
    /// <param name="startingCell">La celda origen de la búsqueda</param>
    public abstract void FindCells(Cell startingCell);

}
