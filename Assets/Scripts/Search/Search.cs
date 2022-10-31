using System.Collections.Generic;

/// <summary>
/// B�squeda de celdas
/// </summary>
public abstract class Search
{

    /// <summary>
    /// Lista con los resultados de la b�squeda
    /// </summary>
    public List<Cell> results;

    /// <summary>
    /// Construye una nueva b�squeda
    /// </summary>
    public Search()
    {
        results = new List<Cell>();
    }

    /// <summary>
    /// Busca celdas a partir de la celda inicial especificada y guarda los resultados en <c>results</c>
    /// </summary>
    /// <param name="startingCell">La celda origen de la b�squeda</param>
    public abstract void FindCells(Cell startingCell);

}
