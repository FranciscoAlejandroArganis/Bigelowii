using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bot�n de la tarjeta de comandos
/// </summary>
public class CommandButton : MonoBehaviour
{

    /// <summary>
    /// Enumeraci�n de los tipos de bot�n
    /// <list type="bullet">
    /// <item><c>Empty</c>: el bot�n es vac�o</item>
    /// <item><c>Command</c>: el bot�n es un comando para que la unidad realice una acci�n</item>
    /// <item><c>Cancel</c>: el bot�n es para cancelar</item>
    /// </list>
    /// </summary>
    public enum Type
    {
        Empty,
        Command,
        Cancel
    }

    /// <summary>
    /// Tipo del bot�n
    /// </summary>
    public Type type;

    /// <summary>
    /// Componente <c>Image</c> del bot�n
    /// </summary>
    public Image image;

    /// <summary>
    /// Componente <c>Button</c> del bot�n
    /// </summary>
    public Button button;

    /// <summary>
    /// Acci�n que se realiza al presionar el bot�n
    /// </summary>
    public Action action;

    /// <summary>
    /// �ndice de la tarjeta a la que se transiciona despu�s de presionar el bot�n
    /// </summary>
    public uint transition;

    public void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Se manda a llamar cuando el jugador presiona el bot�n en la interfaz de usuario
    /// </summary>
    public void OnClick()
    {
        //
    }

}
