using UnityEngine.UIElements;
/// <summary>
/// Bot�n de la tarjeta de comandos
/// </summary>
public class CommandButton : Button
{

    /// <summary>
    /// Enumeraci�n de los tipos de bot�n de la tarjeta de comandos
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
    /// Acci�n que se realiza al presionar el bot�n
    /// </summary>
    public Action action;

    /// <summary>
    /// �ndice de la tarjeta a la que se transiciona despu�s de presionar el bot�n
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
