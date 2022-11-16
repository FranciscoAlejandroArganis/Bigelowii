/// <summary>
/// Acci�n en la una unidad pierde un comportamiento porque se agota su duraci�n
/// </summary>
public abstract class BehaviorExpire : UntargetedAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: se elimna el comportamiento  y se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: termina la acci�n</item>
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
    /// Comportamiento que expira
    /// </summary>
    private Behavior behavior;

    /// <summary>
    /// Construye una nueva acci�n de eliminar comportamiento
    /// </summary>
    /// <param name="behavior">El comportamiento que expira</param>
    public BehaviorExpire(Behavior behavior) : base(behavior.unit)
    {
        this.behavior = behavior;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                behavior.OnRemove();
                unit.behaviors.Remove(behavior);
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

}
