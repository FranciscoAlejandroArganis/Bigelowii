using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Elemento de la interfaz de usuario que puede hacer que se muestre el tooltip
/// </summary>
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    /// <summary>
    /// Indica si el disparador va a mostrar el tooltip cuando el mouse pase encima
    /// </summary>
    public bool active;

    /// <summary>
    /// Sprite que se muestra en el tooltip
    /// </summary>
    public Sprite sprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (active)
        {
            UI.tooltip.trigger = this;
            UI.tooltip.SetSprite();
            UI.tooltip.Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.tooltip.Hide();
    }

}
