using UnityEngine;

/// <summary>
/// Elemento principal de la interfaz de usuario
/// </summary>
public abstract class Panel : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados del panel
    /// <list type="bullet">
    /// <item><c>Hidden</c>: el panel no es visible</item>
    /// <item><c>Timer</c>: el panel está en espera de un temporizador</item>
    /// <item><c>Visible</c>: el panel es visible</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Hidden,
        Timer,
        Visible
    }

    /// <summary>
    /// Estado actual del panel
    /// </summary>
    public State state;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    public float time;

    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qué tan rápido se completa el cambio</para>
    /// </summary>
    public float speed;

    /// <summary>
    /// Objeto que contiene los elementos del panel
    /// <para>Se activa o desactiva este objeto para mostar u ocultar el panel</para>
    /// </summary>
    public GameObject panel;

    /// <summary>
    /// Hace que el panel se muestre en la interfaz de usuario
    /// </summary>
    public abstract void Show();

    /// <summary>
    /// Hace que el panel se oculte en la interfaz de usuario
    /// </summary>
    public abstract void Hide();

    /// <summary>
    /// Actualiza el estado y contenido mostrado del panel
    /// </summary>
    public abstract void UpdatePanel();

}
