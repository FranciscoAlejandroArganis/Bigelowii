using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Componente encargado de manejar el progreso del nivel
/// </summary>
public class Level : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados de un nivel
    /// <list type="bullet">
    /// <item><c>Standby</c>: el nivel no está siendo controlado ni por el jugador humano ni por la IA</item>
    /// <item><c>Spawning</c>: el nivel está evaluando si se deben generar nuevas unidades</item>
    /// <item><c>Event</c>: el nivel avanza al siguiente evento</item>
    /// <item><c>Human</c>: el nivel está en el turno del jugador humano</item>
    /// <item><c>AI</c>: el nivel está en el turno de la IA</item>
    /// <item><c>Completed</c>: el nivel ha terminado</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Standby,
        Spawning,
        Event,
        Human,
        AI,
        Completed
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
    /// Es <c>true</c> si el juego ha terminado en victoria para el jugador humano
    /// </summary>
    private static bool victory;

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
        Timeline.EnqueueLast(new Event(awake, unit.delay));
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
        if (timelineEvent is VictoryEvent)
            return false;
        Action action = timelineEvent.action;
        return action.unit == unit || (action is UnitTargetAction && action.GetTarget().unit == unit);
    }

    /// <summary>
    /// La cantidad de unidades de tiempo que el jugador humano debe sobrevivir
    /// </summary>
    public int timeLimit;

    /// <summary>
    /// El jugador humano
    /// </summary>
    public Player human;

    /// <summary>
    /// Cantidad de conos con los que empieza el jugador
    /// </summary>
    public int startingCones;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    private float time;

    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qué tan rápido se completa el cambio</para>
    /// </summary>
    public float speed;

    public void Start()
    {
        instance = this;
        spawners = GetComponentsInChildren<Spawner>();
        cones = startingCones;
        UI.resources.UpdateCones();
        foreach (Unit unit in GetComponentsInChildren<Unit>())
        {
            Awake awake = new Awake(unit);
            Timeline.EnqueueLast(new Event(awake, unit.initialDelay));
        }
        Timeline.EnqueueLast(new VictoryEvent(timeLimit));
        Timeline.Update();
    }

    public void Update()
    {
        switch (state)
        {
            case State.Spawning:
                if (!human.GetComponentInChildren<Unit>())
                    state = State.Completed;
                else
                {
                    bool generated = false;
                    foreach (Spawner spawner in spawners)
                    {
                        if (currentTime - spawner.time >= spawner.cooldown && spawner.Spawn())
                        {
                            generated = true;
                            spawner.time = currentTime;
                        }
                    }
                    if (generated)
                    {
                        state = State.Standby;
                        Timeline.Update();
                    }
                    else
                        state = State.Event;
                }
                break;
            case State.Event:
                Event timelineEvent = Timeline.Peek();
                if (timelineEvent is VictoryEvent)
                {
                    state = State.Completed;
                    victory = true;
                }
                else
                {
                    Action action = timelineEvent.action;
                    state = State.Standby;
                    action.unit.actionController.StartAction(action);
                }
                break;
            case State.Completed:
                time += speed * Time.deltaTime;
                if (time >= 1)
                    SceneManager.LoadScene(victory ? "Victory" : "Defeat");
                break;
        }
    }

}
