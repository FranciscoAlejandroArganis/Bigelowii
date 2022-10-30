using UnityEngine;

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
    /// Conjunto de sprites
    /// </summary>
    public static SpriteSet sprites;

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
    /// El prefab de un botón de evento
    /// </summary>
    public EventButton eventButton;

    public void Awake()
    {
        instance = this;
        timeline = GetComponentInChildren<TimelinePanel>();
        UnitPanel[] units = GetComponentsInChildren<UnitPanel>();
        primaryUnit = units[0];
        secondaryUnit = units[1];
        sprites = GetComponent<SpriteSet>();
    }

}
