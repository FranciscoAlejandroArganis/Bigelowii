using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel que muestra la l�nea de tiempo
/// </summary>
public class TimelinePanel : Panel
{

    /// <summary>
    /// La acci�n que causa una modificaci�n de la l�nea de tiempo
    /// </summary>
    public Action action;

    /// <summary>
    /// Barra de progreso del nivel
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Lista de botones de evento que se mueven como resultado de cambios en la l�nea de tiempo
    /// </summary>
    private List<EventButton> eventButtons;

    public void Start()
    {
        eventButtons = new List<EventButton>();
    }

    public void Update()
    {
        if (state == State.Timer)
        {
            foreach (EventButton eventButton in eventButtons)
                eventButton.UpdatePosition();
            time += speed * Time.deltaTime;
            if (time >= 1)
                ExitShiftState();
        }
    }

    /// <summary>
    /// Actualiza los botones que se muestran en el panel de la l�nea de tiempo
    /// <para>Se manda a llamar desde <c>Timeline.Update</c> despu�s de que se insertan o eliminan eventos</para>
    /// </summary>
    public void UpdateEventButtons()
    {
        foreach (EventButton eventButton in GetComponentsInChildren<EventButton>())
        {
            if (eventButton.state == EventButton.State.NotPositioned)
                // El bot�n se acaba de crear en el panel y no ha recibido una posici�n
                eventButton.SetPosition();
            else if (!eventButton.TimeMatch())
                // El bot�n ya estaba en la l�nea de tiempo pero necesita moverse
                eventButtons.Add(eventButton);
        }
        if (eventButtons.Count > 0)
        {
            time = 0;
            state = State.Timer;
        }
        else
            ExitShiftState();
    }

    /// <summary>
    /// Regresa la posici�n que debe tener un bot�n de acuerdo al tiempo especificado
    /// </summary>
    /// <param name="time">El tiempo transformado del evento del bot�n</param>
    /// <returns></returns>
    public Vector2 EventButtonPosition(int time)
    {
        return new Vector2(-752 + 32 * time, -12);
    }

    /// <summary>
    /// Recibe una coordena x y regresa <c>true</c> si corresponde a la coordenada x de un bot�n dentro del panel
    /// </summary>
    /// <param name="x">La coordenada x de la posici�n del bot�n en la interfaz de usuario</param>
    /// <returns><c>true</c> si el bot�n est� dentro del panel</returns>
    public bool InPanelBounds(float x)
    {
        return -752 <= x && x <= 752;
    }

    /// <summary>
    /// Sale del estado de desplazamiento
    /// </summary>
    private void ExitShiftState()
    {
        foreach (EventButton eventButton in eventButtons)
            eventButton.FinalPosition();
        eventButtons.Clear();
        state = State.Visible;
        if (action != null)
        {
            Action action = this.action;
            this.action = null;
            action.Execute();
        }
        else
            Level.state = Level.State.Spawning;
    }

    public override void Show()
    {
        throw new NotImplementedException();
    }

    public override void Hide()
    {
        throw new NotImplementedException();
    }

}
