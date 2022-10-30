/// <summary>
/// Evento de la l�nea de tiempo
/// </summary>
public class Event
{

    /// <summary>
    /// El tiempo en el que ocurre el evento
    /// </summary>
    public int time;

    /// <summary>
    /// La acci�n que ocurre en el evento
    /// </summary>
    public Action action;

    /// <summary>
    /// Construye un nuevo evento ccon el tiempo y acci�n especificados
    /// </summary>
    /// <param name="time">El tiempo en el que ocurre el evento</param>
    /// <param name="action">La acci�n que ocurre en el evento</param>
    public Event(Action action, int time)
    {
        this.action = action;
        this.time = time;
    }

    public static bool operator >(Event a, Event b)
    {
        return a.time > b.time;
    }

    public static bool operator <(Event a, Event b)
    {
        return a.time < b.time;
    }

    public static bool operator >=(Event a, Event b)
    {
        return a.time >= b.time;
    }

    public static bool operator <=(Event a, Event b)
    {
        return a.time <= b.time;
    }

}
