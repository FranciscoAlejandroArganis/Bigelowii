using System;
using UnityEngine;

/// <summary>
/// B�squeda de un camino a la celda que minimiza una funci�n
/// </summary>
public class MinimumSearch : BreadthFirstSearch
{

    /// <summary>
    /// Celda con el menor valor de la funci�n
    /// </summary>
    public Cell minimum;

    /// <summary>
    /// Valor m�nimo de la funci�n
    /// </summary>
    public uint value;

    /// <summary>
    /// Funci�n que se eval�a con las celdas durante la b�squeda
    /// </summary>
    private Func<Cell, uint> function;

    public MinimumSearch(Func<Cell, uint> function) : base()
    {
        this.function = function;
    }

    public override void FindCells(Cell startingCell)
    {
        minimum = startingCell;
        value = function(startingCell);
        base.FindCells(startingCell);
    }

    protected override bool Edge(Cell currentCell, Cell nextCell)
    {
        if (value == 0)
            return false;
        if (nextCell.IsFree())
        {
            nextCell.distance = currentCell.distance + 1;
            nextCell.predecesor = currentCell;
            uint nextValue = function(nextCell);
            if (nextValue < value)
            {
                minimum = nextCell;
                value = nextValue;
            }
            return true;
        }
        return false;
    }

}
