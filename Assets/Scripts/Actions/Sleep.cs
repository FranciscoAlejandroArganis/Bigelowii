using System;

/// <summary>
/// Acci�n en la que una unidad termina su turno actual
/// </summary>
public class Sleep : UntargetedAction
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
    /// Construye una nueva acci�n de terminar turno
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    public Sleep(Unit unit) : base(unit) { }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                Awake awake = new Awake(unit);
                Timeline.EnqueueLast(new Event(awake, unit.delay));
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                Turn.activeUnit = null;
                unit.actionsTaken = 0;
                unit.actionController.StopAction();
                break;
        }
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }
}
