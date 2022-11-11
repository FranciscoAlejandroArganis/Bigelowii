using UnityEngine.UI;

/// <summary>
/// Bot�n de la interfaz de usuario
/// </summary>
public abstract class Button : TooltipTrigger
{

    /// <summary>
    /// Imagen del bot�n
    /// </summary>
    public Image image;

    /// <summary>
    /// Componente interactivo del bot�n
    /// </summary>
    public UnityEngine.UI.Button button;

    public virtual void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<UnityEngine.UI.Button>();
    }

    /// <summary>
    /// Se manda a llamar cuando el jugador presiona el bot�n en la interfaz de usuario
    /// </summary>
    public abstract void OnClick();

}
