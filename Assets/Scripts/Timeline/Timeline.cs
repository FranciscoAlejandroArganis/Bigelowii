using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Línea de tiempo
/// </summary>
public class Timeline
{

    /// <summary>
    /// Lista ordenada de eventos de la línea de tiempo
    /// </summary>
    public static List<Event> events = new List<Event>();

    /// <summary>
    /// Predicado que indica si un evento debe eliminarse
    /// </summary>
    private static Predicate<Event> predicate;

    /// <summary>
    /// Agrega el evento especificado desde el final de la línea de tiempo
    /// <para>El evento se inserta en su posición ordenada</para>
    /// </summary>
    /// <param name="timelineEvent">El evento que se agrega</param>
    public static void EnqueueLast(Event timelineEvent)
    {
        events.Add(timelineEvent);
        InsertLeft(events.Count - 1);
    }

    /// <summary>
    /// Agrega el evento especificado desde el inicio de la línea de tiempo
    /// <para>El evento se inserta en su posición ordenada</para>
    /// </summary>
    /// <param name="timelineEvent">El evento que se agrega</param>
    public static void EnqueueFirst(Event timelineEvent)
    {
        events.Insert(0, timelineEvent);
        InsertRight(0);
    }

    /// <summary>
    /// Regresa el primer evento de la línea de tiempo
    /// </summary>
    /// <returns>El siguiente evento que debe ocurrir</returns>
    public static Event Peek()
    {
        return events[0];
    }

    /// <summary>
    /// Elimina de la línea de tiempo el primer evento
    /// </summary>
    public static void Dequeue()
    {
        Level.IncreaseTime(events[1].time);
        events[0].eventButton.time = -(events[1].time + 1);
        events.RemoveAt(0);
    }

    /// <summary>
    /// Actualiza la línea de tiempo después de que se han modificado los eventos
    /// </summary>
    public static void Update()
    {
        int time = events[0].time;
        int index = 0;
        foreach (Event timelineEvent in events)
        {
            timelineEvent.time -= time;
            timelineEvent.eventButton.time = timelineEvent.time + index;
            index++;
        }
        UI.timeline.UpdatePanel();
    }

    /// <summary>
    /// Elimina de la línea de tiempo todos los eventos que satisfacen el predicado especificado
    /// </summary>
    /// <param name="predicate">El predicado que indica si un evento debe eliminarse</param>
    public static void RemoveEvents(Predicate<Event> predicate)
    {
        Timeline.predicate = predicate;
        events.RemoveAll(EventMatch);
        Timeline.predicate = null;
    }

    /// <summary>
    /// Inserta hacia atrás el evento en <c>index</c>
    /// </summary>
    /// <param name="index">El índice del evento que se inserta</param>
    private static void InsertLeft(int index)
    {
        Event timelineEvent = events[index];
        if (index > 0 && events[index - 1] > timelineEvent)
        {
            while (true)
            {
                events[index] = events[index - 1];
                index--;
                if (index == 0 || events[index - 1] <= timelineEvent)
                    break;
            }
            events[index] = timelineEvent;
        }
    }

    /// <summary>
    /// Inserta hacia adelante el evento en <c>index</c>
    /// </summary>
    /// <param name="index">El índice del evento que se inserta</param>
    private static void InsertRight(int index)
    {
        Event timelineEvent = events[index];
        if (index + 1 < events.Count && timelineEvent > events[index + 1])
        {
            while (true)
            {
                events[index] = events[index + 1];
                index++;
                if (index + 1 == events.Count || timelineEvent <= events[index + 1])
                    break;
            }
            events[index] = timelineEvent;
        }
    }

    /// <summary>
    /// Determina si el evento especifcado se debe eliminar de la línea de tiempo
    /// <para>Si el evento debe eliminarse, se destruye su botón</para>
    /// </summary>
    /// <param name="timelineEvent">El evento que se prueba</param>
    /// <returns><c>true</c> si el evento debe eliminarse</returns>
    private static bool EventMatch(Event timelineEvent)
    {
        if (predicate(timelineEvent))
        {
            timelineEvent.eventButton.animator.SetTrigger("Disappear");
            GameObject.Destroy(timelineEvent.eventButton.gameObject, 1);
            return true;
        }
        return false;
    }

}
