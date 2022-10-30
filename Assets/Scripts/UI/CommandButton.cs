using UnityEngine.UIElements;
/// <summary>
/// Botón de la tarjeta de comandos
/// </summary>
public class CommandButton : Button
{

    /// <summary>
    /// Enumeración de los tipos de botón de la tarjeta de comandos
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
    /// Acción que se realiza al presionar el botón
    /// </summary>
    public Action action;

    /// <summary>
    /// Índice de la tarjeta a la que se transiciona después de presionar el botón
    /// </summary>
    public uint transition;

    public override void OnClick()
    {
        if (Level.state == Level.State.Human)
        {
            if (type == Type.Cancel)
                Turn.CancelAction();
            else if (Turn.SelectAction(action))
                UI.primaryUnit.SetCommandCard(transition);
        }
    }

}
