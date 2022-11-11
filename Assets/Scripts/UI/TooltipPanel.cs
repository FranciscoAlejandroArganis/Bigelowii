using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel que muestra el tooltip
/// </summary>
public class TooltipPanel : Panel
{

    /// <summary>
    /// Elemento que muestra el tooltip
    /// </summary>
    public TooltipTrigger trigger;

    /// <summary>
    /// Componente <c>Image</c> que muestra el sprite del tooltip
    /// </summary>
    public Image image;

    /// <summary>
    /// Contiene la posición en la interfaz de usuario del tooltip
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Contiene la posición en la interfaz de usuario del lienzo
    /// </summary>
    private RectTransform canvas;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<RectTransform>();
    }

    public void Update()
    {
        switch (state)
        {
            case State.Hidden:
                break;
            case State.Timer:
                time += speed * Time.deltaTime;
                if (time >= 1)
                {
                    UpdatePosition();
                    panel.SetActive(true);
                    state = State.Visible;
                }
                break;
            case State.Visible:
                UpdatePosition();
                break;
        }
    }

    public override void Show()
    {
        time = 0;
        state = State.Timer;
    }

    public override void Hide()
    {
        trigger = null;
        if (panel)
            panel.SetActive(false);
        state = State.Hidden;
    }

    public override void UpdatePanel()
    {
        if (trigger)
        {
            if (trigger.active)
                SetSprite();
            else
                Hide();
        }
    }

    /// <summary>
    /// Hace que el tooltip muestre el sprite del disparador actual
    /// </summary>
    public void SetSprite()
    {
        image.sprite = trigger.sprite;
    }

    /// <summary>
    /// Actualiza la posición del tooltip en la interfaz de usuario
    /// </summary>
    private void UpdatePosition()
    {
        Vector2 position = Input.mousePosition / transform.parent.localScale.x;
        if (position.x + rectTransform.sizeDelta.x > canvas.sizeDelta.x)
            position.x = canvas.sizeDelta.x - rectTransform.sizeDelta.x;
        if (position.y + rectTransform.sizeDelta.y > canvas.sizeDelta.y)
            position.y = canvas.sizeDelta.y - rectTransform.sizeDelta.y;
        rectTransform.anchoredPosition = position;
    }

}
