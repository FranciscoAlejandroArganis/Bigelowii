using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel que muestra la línea de tiempo
/// </summary>
public class TimelinePanel : Panel
{

    /// <summary>
    /// La acción que causa una modificación de la línea de tiempo
    /// </summary>
    public Action action;

    /// <summary>
    /// Barra de progreso del nivel
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Lista de botones de evento que se mueven como resultado de cambios en la línea de tiempo
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
    /// Actualiza los botones que se muestran en el panel de la línea de tiempo
    /// <para>Se manda a llamar desde <c>Timeline.Update</c> después de que se insertan o eliminan eventos</para>
    /// </summary>
    public void UpdateEventButtons()
    {
        foreach (EventButton eventButton in GetComponentsInChildren<EventButton>())
        {
            if (eventButton.state == EventButton.State.NotPositioned)
                // El botón se acaba de crear en el panel y no ha recibido una posición
                eventButton.SetPosition();
            else if (!eventButton.TimeMatch())
                // El botón ya estaba en la línea de tiempo pero necesita moverse
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
    /// Regresa la posición que debe tener un botón de acuerdo al tiempo especificado
    /// </summary>
    /// <param name="time">El tiempo transformado del evento del botón</param>
    /// <returns></returns>
    public Vector2 EventButtonPosition(int time)
    {
        return new Vector2(-752 + 32 * time, -12);
    }

    /// <summary>
    /// Recibe una coordena x y regresa <c>true</c> si corresponde a la coordenada x de un botón dentro del panel
    /// </summary>
    /// <param name="x">La coordenada x de la posición del botón en la interfaz de usuario</param>
    /// <returns><c>true</c> si el botón está dentro del panel</returns>
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
