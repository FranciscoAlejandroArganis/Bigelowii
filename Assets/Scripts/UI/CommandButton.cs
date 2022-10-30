using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Botón de la tarjeta de comandos
/// </summary>
public class CommandButton : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los tipos de botón
    /// <list type="bullet">
    /// <item><c>Empty</c>: el botón es vacío</item>
    /// <item><c>Command</c>: el botón es un comando para que la unidad realice una acción</item>
    /// <item><c>Cancel</c>: el botón es para cancelar</item>
    /// </list>
    /// </summary>
    public enum Type
    {
        Empty,
        Command,
        Cancel
    }

    /// <summary>
    /// Tipo del botón
    /// </summary>
    public Type type;

    /// <summary>
    /// Componente <c>Image</c> del botón
    /// </summary>
    public Image image;

    /// <summary>
    /// Componente <c>Button</c> del botón
    /// </summary>
    public Button button;

    /// <summary>
    /// Acción que se realiza al presionar el botón
    /// </summary>
    public Action action;

    /// <summary>
    /// Índice de la tarjeta a la que se transiciona después de presionar el botón
    /// </summary>
    public uint transition;

    public void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Se manda a llamar cuando el jugador presiona el botón en la interfaz de usuario
    /// </summary>
    public void OnClick()
    {
        //
    }

}
