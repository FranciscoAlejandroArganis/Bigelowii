using UnityEngine;

/// <summary>
/// Componente encargado de manejar el progreso del nivel
/// </summary>
public class LevelHandler : MonoBehaviour
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

    public void Start()
    {
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
