/// <summary>
/// Acción en la una unidad pierde un comportamiento porque se agota su duración
/// </summary>
public abstract class BehaviorExpire : UntargetedAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: se elimna el comportamiento  y se actualiza la línea de tiempo</item>
    /// <item><c>End</c>: termina la acción</item>
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
    /// Comportamiento que expira
    /// </summary>
    private Behavior behavior;

    /// <summary>
    /// Construye una nueva acción de eliminar comportamiento
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
