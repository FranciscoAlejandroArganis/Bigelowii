using UnityEngine.UI;

/// <summary>
/// Botón de la interfaz de usuario
/// </summary>
public abstract class Button : TooltipTrigger
{

    /// <summary>
    /// Imagen del botón
    /// </summary>
    public Image image;

    /// <summary>
    /// Componente interactivo del botón
    /// </summary>
    public UnityEngine.UI.Button button;

    public virtual void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<UnityEngine.UI.Button>();
    }

    /// <summary>
    /// Se manda a llamar cuando el jugador presiona el botón en la interfaz de usuario
    /// </summary>
    public abstract void OnClick();

}
