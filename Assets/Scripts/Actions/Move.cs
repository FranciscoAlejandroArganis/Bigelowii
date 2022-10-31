using System;

/// <summary>
/// Acción en la que una unidad se mueve hacia la celda objetivo
/// </summary>
public class Move : CellTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: empieza el desplazamiento a través del camino</item>
    /// <item><c>End</c>: la unidad ha alcanzado su destino</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        End
    }

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Arreglo con el camino de celdas por las que se va a mover la unidad
    /// </summary>
    private Cell[] path;

    /// <summary>
    /// Contador de las veces que se ha llamado al método <c>GetTarget</c>
    /// </summary>
    private uint counter;

    /// <summary>
    /// Construye una nueva acción de moverse
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public Move(Unit unit) : base(unit)
    {
        search = new PathSearch(unit.movement);
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                unit.movementController.MoveThrough(path);
                CameraController.Follow(unit);
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    public override void SetTarget(Cell target)
    {
        path = PathSearch.BuildPathTo(target);
        targetCell = target;
    }

    public override Cell GetTarget()
    {
        if (counter < 2)
        {
            counter++;
            return counter == 1 ? targetCell : path[1];
        }
        return targetCell;
    }

    public override void ClearTargets()
    {
        foreach (Cell cell in search.results)
        {
            cell.visited = false;
            cell.distance = 0;
            cell.predecesor = null;
            cell.highlight.Remove(Highlight.State.Target);
        }
        search.results.Clear();
    }

    protected override bool ValidTarget(Cell cell)
    {
        return cell != unit.cell;
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

    public override void AddTargetHighlight(Cell cell)
    {
        cell.highlight.Add(Highlight.State.SelectedTarget);
        if (path == null)
        {
            while (cell.predecesor)
            {
                cell = cell.predecesor;
                cell.highlight.Add(Highlight.State.AreaOfEffect);
            }
        }
    }

    public override void RemoveTargetHighlight(Cell cell)
    {
        cell.highlight.Remove(Highlight.State.SelectedTarget);
        if (path == null)
        {
            while (cell.predecesor)
            {
                cell = cell.predecesor;
                cell.highlight.Remove(Highlight.State.AreaOfEffect);
            }
        }
        else if (path[0] == unit.cell)
        {
            // Si el camino ya se asignó pero la unidad está en la celda inicial, se está llamando a este método desde Turn.SelectTarget
            int index = path.Length - 2;
            while (index >= 0)
            {
                path[index].highlight.Remove(Highlight.State.AreaOfEffect);
                index--;
            }
        }
    }

}
