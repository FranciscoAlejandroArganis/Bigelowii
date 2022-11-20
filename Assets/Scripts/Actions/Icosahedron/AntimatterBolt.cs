using System;
using UnityEngine;

/// <summary>
/// Acción en la que un icosaedro inicia la preparación para atacar
/// <para>El icosaedro atacará 3 unidades de tiempo después</para>
/// <para>Termina inmediatamente el turno actual del icosaedro</para>
/// </summary>
public class AntimatterBolt : CellTargetAction
{

    /// <summary>
    /// Unidades de tiempo que dura la preparación y que tiene que esperar el icosaedro antes de atacar
    /// </summary>
    private static int delay = 3;

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el icosaedro inicia la preparación y se actualiza la línea de tiempo</item>
    /// <item><c>End</c>: el icosaedro termina su turno</item>
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
    /// Plantilla usada para generar el proyectil del ataque
    /// </summary>
    protected ParabolicProjectile bolt;

    /// <summary>
    /// Construye una nueva acción <c>AntimaterBolt</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    /// <param name="bolt">La plantilla usada para generar el proyectil del ataque</param>
    public AntimatterBolt(Unit unit, ParabolicProjectile bolt) : base(unit)
    {
        search = new EuclideanDistanceSearch(4);
        this.bolt = bolt;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                AntimatterBoltDelayed delayed = new AntimatterBoltDelayed(unit, targetCell, bolt);
                Timeline.EnqueueLast(new Event(delayed, unit.delay < delay ? unit.delay : delay));
                Awake awake = new Awake(unit);
                Timeline.EnqueueLast(new Event(awake, unit.delay));
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                Turn.activeUnit = null;
                unit.actionsTaken = Level.TechnologyMask(unit);
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        return true;
    }

    public override void AddTargetHighlight(Cell cell)
    {
        cell.highlight.Add(Highlight.State.SelectedTarget);
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 2, Utilities.mapLayer))
            collider.GetComponent<Cell>().highlight.Add(Highlight.State.AreaOfEffect);
    }

    public override void RemoveTargetHighlight(Cell cell)
    {
        cell.highlight.Remove(Highlight.State.SelectedTarget);
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 2, Utilities.mapLayer))
            collider.GetComponent<Cell>().highlight.Remove(Highlight.State.AreaOfEffect);
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
