using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Componente encargado de manejar la interfaz de usario
/// </summary>
public class UI : MonoBehaviour
{

    /// <summary>
    /// Panel de la l�nea de tiempo
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
    /// Ejemplar �nico de <c>UI</c>
    /// </summary>
    private static UI instance;

    /// <summary>
    /// Regresa un nuevo bot�n de evento
    /// </summary>
    /// <returns>Un nuevo bot�n de evento</returns>
    public static EventButton NewEventButton()
    {
        return Instantiate(instance.eventButton, timeline.panel.transform);
    }

    /// <summary>
    /// Regresa un nuevo �cono de cono
    /// </summary>
    /// <param name="transform">La transformaci�n del objeto padre del nuevo �cono de cono</param>
    /// <returns>Un nuevo �cono de cono</returns>
    public static Image NewConeIcon(Transform transform)
    {
        return Instantiate(instance.coneIcon, transform);
    }

    /// <summary>
    /// El prefab de un bot�n de evento
    /// </summary>
    public EventButton eventButton;

    /// <summary>
    /// El prefab del �cono de un cono
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
