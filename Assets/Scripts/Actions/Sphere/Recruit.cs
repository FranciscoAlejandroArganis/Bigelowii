using System;

/// <summary>
/// Acci�n en la que una esfera recluta una nueva unidad
/// </summary>
public class Recruit : CellTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: la unidad termina su turno</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        End
    }

    /// <summary>
    /// Estado actual de la acci�n
    /// </summary>
    private State state;

    /// <summary>
    /// Plantilla de la unidad que aparecer�
    /// </summary>
    private Unit template;

    /// <summary>
    /// Construye una nueva acci�n <c>Recruit</c>
    /// </summary>
    /// <param name="sphere">La esfera que realiza la acci�n</param>
    /// <param name="template">La unidad plantilla que se usar� para crear la nueva unidad</param>
    public Recruit(Sphere sphere, Unit template) : base(sphere)
    {
        search = new EuclideanDistanceSearch(3);
        this.template = template;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                Level.NewUnit(template, unit.player, targetCell);
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        return cell.IsFree();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
