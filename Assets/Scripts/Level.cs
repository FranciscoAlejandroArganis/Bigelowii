using UnityEngine;

/// <summary>
/// Componente encargado de manejar el progreso del nivel
/// </summary>
public class Level : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados de un nivel
    /// <list type="bullet">
    /// <item><c>Standby</c>: el juego no está siendo controlado ni por el jugador humano ni por la IA</item>
    /// <item><c>Event</c>: el juego avanza al siguiente evento</item>
    /// <item><c>Human</c>: el juego está en el turno del jugador humano</item>
    /// <item><c>AI</c>: el juego está en el turno de la IA</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Standby,
        Event,
        Human,
        AI
    }

    /// <summary>
    /// Estado actual del nivel
    /// </summary>
    public static State state;

    /// <summary>
    /// Cantidad de conos que tiene actualmente el jugador
    /// </summary>
    public static int cones;

    /// <summary>
    /// La unidad que se elimina del nivel
    /// </summary>
    private static Unit unit;

    /// <summary>
    /// La cantidad de unidades de tiempo que han pasado desde el inicio del nivel
    /// </summary>
    private static int currentTime;

    /// <summary>
    /// Ejemplar único de <c>Level</c>
    /// </summary>
    private static Level instance;

    /// <summary>
    /// Aumenta el tiempo transcurrido en la cantidad especificada
    /// </summary>
    /// <param name="time">La cantidad de unidades de tiempo que aumenta el tiempo transcurrido</param>
    public static void IncreaseTime(int time)
    {
        currentTime += time;
        UI.timeline.slider.value = (float)currentTime / instance.timeLimit;
    }

    /// <summary>
    /// Elimina la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad que se elimina del nivel</param>
    public static void Kill(Unit unit)
    {
        Level.unit = unit;
        Timeline.RemoveEvents(InvolvesUnit);
        Level.unit = null;
        if (unit.animator)
            unit.animator.SetTrigger("Death");
        else
            Destroy(unit.gameObject);
    }

    /// <summary>
    /// Predicado que determina si el evento especificado involucra a <c>unit</c>
    /// </summary>
    /// <param name="timelineEvent">El evento que se prueba</param>
    /// <returns><c>true</c> si en el evento <c>unit</c> realiza la acción o <c>unit</c> es el objetivo de la acción</returns>
    private static bool InvolvesUnit(Event timelineEvent)
    {
        Action action = timelineEvent.action;
        return action.unit == unit || (action is UnitTargetAction && action.GetTarget().unit == unit);
    }

    /// <summary>
    /// La cantidad de unidades de tiempo que el jugador debe sobrevivir
    /// </summary>
    public uint timeLimit;

    public void Start()
    {
        instance = this;
        foreach(Unit unit in GetComponentsInChildren<Unit>())
        {
            Awake awake = new Awake(unit);
            Timeline.EnqueueLast(new Event(awake, unit.initialDelay));
        }
        Timeline.Update();
        state = State.Event;
    }

    public void Update()
    {
        if (state == State.Event)
        {
            Action action = Timeline.Peek().action;
            state = State.Standby;
            action.unit.actionController.StartAction(action);
        }
    }

}
