using UnityEngine;

/// <summary>
/// Bot�n de evento
/// </summary>
public class EventButton : Button
{

    /// <summary>
    /// Enumeraci�n de los estados de un bot�n de evento
    /// <list type="bullet">
    /// <item><c>NotPositioned</c>: el bot�n no ha recibido una posici�n en el panel de la l�nea de tiempo</item>
    /// <item><c>Hidden</c>: el bot�n no es visible en el panel de la l�nea de tiempo</item>
    /// <item><c>Visible</c>: el bot�n es visible en el panel de la l�nea de tiempo</item>
    /// </list>
    /// </summary>
    public enum State
    {
        NotPositioned,
        Hidden,
        Visible
    }

    /// <summary>
    /// Estado actual del bot�n
    /// </summary>
    public State state;

    /// <summary>
    /// Acci�n que ocurre en el evento del bot�n
    /// </summary>
    public Action action;

    /// <summary>
    /// El tiempo transformado del evento
    /// <para>Determina la posici�n dentro del panel de la l�nea de tiempo</para>
    /// <para>El tiempo transformado es el tiempo real del evento m�s el �ndice del evento en la lista ordenada de eventos</para>
    /// </summary>
    public int time;

    /// <summary>
    /// Animador del bot�n
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Contiene la posici�n en la interfaz de usuario del bot�n
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Posici�n inicial del bot�n antes del cambio
    /// </summary>
    private Vector2 positionStart;

    /// <summary>
    /// Posici�n final del bot�n antes del cambio
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
    /// Establece inmediatamente la posici�n del bot�n a la indicada por el tiempo actual
    /// </summary>
    public void SetPosition()
    {
        rectTransform.anchoredPosition = UI.timeline.EventButtonPosition(time);
        state = State.Hidden;
    }

    /// <summary>
    /// Determina si el bot�n se encuentra en la posici�n indicada por su tiempo actual
    /// </summary>
    /// <returns><c>true</c> si la posici�n del bot�n coincide con su tiempo actual</returns>
    public bool TimeMatch()
    {
        positionStart = rectTransform.anchoredPosition;
        positionEnd = UI.timeline.EventButtonPosition(time);
        return positionEnd == positionStart;
    }

    /// <summary>
    /// Se llama mientras cambia la posici�n del bot�n
    /// </summary>
    public void UpdatePosition()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(positionStart, positionEnd, UI.timeline.time);
    }

    /// <summary>
    /// Se llama cuando termina de cambiar la posici�n del bot�n
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
