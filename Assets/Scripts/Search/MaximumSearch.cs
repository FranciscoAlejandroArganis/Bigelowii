using System;

/// <summary>
/// Búsqueda de un camino a la celda que maximiza una función
/// </summary>
public class MaximumSearch : BreadthFirstSearch
{

    /// <summary>
    /// Celda con el mayor valor de la función
    /// </summary>
    public Cell maximum;

    /// <summary>
    /// Valor máximo de la función
    /// </summary>
    public uint value;

    /// <summary>
    /// Función que se evalúa con las celdas durante la búsqueda
    /// </summary>
    private Func<Cell, uint> function;

    public MaximumSearch(Func<Cell, uint> function) : base()
    {
        this.function = function;
    }

    public override void FindCells(Cell startingCell)
    {
        maximum = startingCell;
        value = function(startingCell);
        base.FindCells(startingCell);
    }

    protected override bool Edge(Cell currentCell, Cell nextCell)
    {
        if (value == uint.MaxValue)
            return false;
        if (nextCell.IsFree())
        {
            nextCell.distance = currentCell.distance + 1;
            nextCell.predecesor = currentCell;
            uint nextValue = function(nextCell);
            if (nextValue > value)
            {
                maximum = nextCell;
                value = nextValue;
            }
            return true;
        }
        return false;
    }

}
