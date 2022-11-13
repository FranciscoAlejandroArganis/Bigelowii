using System;

/// <summary>
/// Búsqueda de un camino a la primer celda que satisface un predicado
/// </summary>
public class FirstMatchSearch : BreadthFirstSearch
{
    
    /// <summary>
    /// Primer celda que satisface el predicado
    /// <para>Es <c>null</c> si no se ha encontrado una celda que satisface el predicado</para>
    /// </summary>
    public Cell firstMatch;

    /// <summary>
    /// Predicado con el que se prueban las celdas durante la búsqueda
    /// </summary>
    private Predicate<Cell> predicate;

    /// <summary>
    /// Construye una nueva búsqueda que encuentra un camino a la primer celda que satisface un predicado
    /// </summary>
    /// <param name="predicate">El predicado con el que se prueban las celdas durante la búsqueda</param>
    public FirstMatchSearch(Predicate<Cell> predicate) : base()
    {
        this.predicate = predicate;
    }

    public override void FindCells (Cell startingCell)
    {
        if (predicate(startingCell))
        {
            firstMatch = startingCell;
            results.Add(startingCell);
        }
        else
            base.FindCells(startingCell);
    }

    protected override bool Edge(Cell currentCell, Cell nextCell)
    {
        if (firstMatch)
            return false;
        if (nextCell.IsFree())
        {
            nextCell.distance = currentCell.distance + 1;
            nextCell.predecesor = currentCell;
            if (predicate(nextCell))
                firstMatch = nextCell;
            return true;
        }
        return false;
    }

}
