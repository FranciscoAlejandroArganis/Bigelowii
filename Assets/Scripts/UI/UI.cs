using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Componente encargado de manejar la interfaz de usario
/// </summary>
public class UI : MonoBehaviour
{

    /// <summary>
    /// Panel de la línea de tiempo
    /// </summary>
    public static TimelinePanel timeline;

    /// <summary>
    /// Panel de la unidad primaria
    /// </summary>
    public static UnitPanel primaryUnit;

    /// <summary>
    /// Panel de la unidad secundaria
    /// </summary>
    public static UnitPanel secondaryUnit;

    /// <summary>
    /// Panel de recursos
    /// </summary>
    public static ResourcesPanel resources;

    /// <summary>
    /// Panel del tooltip
    /// </summary>
    public static TooltipPanel tooltip;

    /// <summary>
    /// Conjunto de sprites
    /// </summary>
    public static SpritesSet sprites;

    /// <summary>
    /// Ejemplar único de <c>UI</c>
    /// </summary>
    private static UI instance;

    /// <summary>
    /// Regresa un nuevo botón de evento
    /// </summary>
    /// <returns>Un nuevo botón de evento</returns>
    public static EventButton NewEventButton()
    {
        return Instantiate(instance.eventButton, timeline.panel.transform);
    }

    /// <summary>
    /// Regresa un nuevo ícono de cono
    /// </summary>
    /// <param name="transform">La transformación del objeto padre del nuevo ícono de cono</param>
    /// <returns>Un nuevo ícono de cono</returns>
    public static Image NewConeIcon(Transform transform)
    {
        return Instantiate(instance.coneIcon, transform);
    }

    /// <summary>
    /// El prefab de un botón de evento
    /// </summary>
    public EventButton eventButton;

    /// <summary>
    /// El prefab del ícono de un cono
    /// </summary>
    public Image coneIcon;

    public void Awake()
    {
        instance = this;
        timeline = GetComponentInChildren<TimelinePanel>();
        UnitPanel[] units = GetComponentsInChildren<UnitPanel>();
        primaryUnit = units[0];
        secondaryUnit = units[1];
        resources = GetComponentInChildren<ResourcesPanel>();
        tooltip = GetComponentInChildren<TooltipPanel>();
    }

}
