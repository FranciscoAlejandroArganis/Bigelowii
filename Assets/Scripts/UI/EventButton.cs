using UnityEngine;

/// <summary>
/// Botón de evento
/// </summary>
public class EventButton : Button
{

    /// <summary>
    /// Enumeración de los estados de un botón de evento
    /// <list type="bullet">
    /// <item><c>NotPositioned</c>: el botón no ha recibido una posición en el panel de la línea de tiempo</item>
    /// <item><c>Hidden</c>: el botón no es visible en el panel de la línea de tiempo</item>
    /// <item><c>Visible</c>: el botón es visible en el panel de la línea de tiempo</item>
    /// </list>
    /// </summary>
    public enum State
    {
        NotPositioned,
        Hidden,
        Visible
    }

    /// <summary>
    /// Estado actual del botón
    /// </summary>
    public State state;

    /// <summary>
    /// Acción que ocurre en el evento del botón
    /// </summary>
    public Action action;

    /// <summary>
    /// El tiempo transformado del evento
    /// <para>Determina la posición dentro del panel de la línea de tiempo</para>
    /// <para>El tiempo transformado es el tiempo real del evento más el índice del evento en la lista ordenada de eventos</para>
    /// </summary>
    public int time;

    /// <summary>
    /// Animador del botón
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Contiene la posición en la interfaz de usuario del botón
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Posición inicial del botón antes del cambio
    /// </summary>
    private Vector2 positionStart;

    /// <summary>
    /// Posición final del botón antes del cambio
    /// </summary>
    private Vector2 positionEnd;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        switch (state)
        {
            case State.NotPositioned:
                break;
            case State.Hidden:
                if (UI.timeline.InPanelBounds(rectTransform.anchoredPosition.x))
                {
                    animator.SetTrigger("Appear");
                    button.interactable = true;
                    state = State.Visible;
                }
                break;
            case State.Visible:
                if (!UI.timeline.InPanelBounds(rectTransform.anchoredPosition.x))
                {
                    animator.SetTrigger("Disappear");
                    button.interactable = false;
                    state = State.Hidden;
                }
                break;
        }
    }

    /// <summary>
    /// Establece inmediatamente la posición del botón a la indicada por el tiempo actual
    /// </summary>
    public void SetPosition()
    {
        rectTransform.anchoredPosition = UI.timeline.EventButtonPosition(time);
        state = State.Hidden;
    }

    /// <summary>
    /// Determina si el botón se encuentra en la posición indicada por su tiempo actual
    /// </summary>
    /// <returns><c>true</c> si la posición del botón coincide con su tiempo actual</returns>
    public bool TimeMatch()
    {
        positionStart = rectTransform.anchoredPosition;
        positionEnd = UI.timeline.EventButtonPosition(time);
        return positionEnd == positionStart;
    }

    /// <summary>
    /// Se llama mientras cambia la posición del botón
    /// </summary>
    public void UpdatePosition()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(positionStart, positionEnd, UI.timeline.time);
    }

    /// <summary>
    /// Se llama cuando termina de cambiar la posición del botón
    /// </summary>
    public void FinalPosition()
    {
        rectTransform.anchoredPosition = positionEnd;
        if (time < 0)
            Destroy(gameObject);
    }

    public override void OnClick()
    {
        if (Level.state == Level.State.Human && action != null)
        {
            if (Turn.state != Turn.State.Unit)
            {
                if (Turn.state == Turn.State.Target)
                    Turn.CancelAction();
                Turn.DeselectUnit();
            }
            Turn.SelectUnit(action.unit);
        }
    }

}
