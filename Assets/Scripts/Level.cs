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
    /// <item><c>Spawning</c>: el juego está evaluando si se deben generar nuevas unidades</item>
    /// <item><c>Event</c>: el juego avanza al siguiente evento</item>
    /// <item><c>Human</c>: el juego está en el turno del jugador humano</item>
    /// <item><c>AI</c>: el juego está en el turno de la IA</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Standby,
        Spawning,
        Event,
        Human,
        AI
    }

    /// <summary>
    /// Estado actual del nivel
    /// </summary>
    public static State state;

    /// <summary>
    /// Cantidad de conos que tiene actualmente el jugador humano
    /// </summary>
    public static int cones;

    /// <summary>
    /// La cantidad de unidades de tiempo que han transcurrido desde el inicio del nivel
    /// </summary>
    public static int currentTime;

    /// <summary>
    /// Arreglo con los generadores de unidades del nivel
    /// </summary>
    private static Spawner[] spawners;

    /// <summary>
    /// La unidad que se elimina del nivel
    /// </summary>
    private static Unit unit;

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
    /// Agrega una nueva unidad al nivel
    /// </summary>
    /// <param name="unit">La unidad plantilla usada para crear la nueva unidad</param>
    /// <param name="player">El jugador al que pertenecerá la nueva unidad</param>
    /// <param name="cell">La celda donde aparecerá la nueva unidad</param>
    /// <returns>La nueva unidad creada</returns>
    public static Unit NewUnit(Unit unit, Player player, Cell cell)
    {
        unit = Instantiate(unit, cell.UnitPosition(unit), Quaternion.identity, player.transform);
        unit.player = player;
        cell.unit = unit;
        Awake awake = new Awake(unit);
        Timeline.EnqueueLast(new Event(awake, unit.initialDelay));
        return unit;
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
        unit.cell.unit = null;
        if (unit.animator)
            unit.animator.SetTrigger("Death");
        else
            unit.Destroy();
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
    /// La cantidad de unidades de tiempo que el jugador humano debe sobrevivir
    /// </summary>
    public uint timeLimit;

    public void Start()
    {
        instance = this;
        spawners = GetComponentsInChildren<Spawner>();
        foreach (Unit unit in GetComponentsInChildren<Unit>())
        {
            Awake awake = new Awake(unit);
            Timeline.EnqueueLast(new Event(awake, unit.initialDelay));
        }
        Timeline.Update();
    }

    public void Update()
    {
        switch (state)
        {
            case State.Spawning:
                bool generated = false;
                foreach(Spawner spawner in spawners)
                {
                    if (currentTime - spawner.time >= spawner.cooldown && spawner.Spawn())
                    {
                        generated = true;
                        spawner.time = currentTime;
                    }
                }
                if (generated)
                    Timeline.Update();
                else
                    state = State.Event;
                break;
            case State.Event:
                Action action = Timeline.Peek().action;
                state = State.Standby;
                action.unit.actionController.StartAction(action);
                break;
        }
    }

}
