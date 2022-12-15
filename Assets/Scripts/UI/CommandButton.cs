using System.ComponentModel;
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
        Confirm,
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

    /// <summary>
    /// �ndice del bot�n dentro de la tarjeta de comandos
    /// </summary>
    public int index;

    public override void OnClick()
    {
        bool invalid = false;
        if (Level.state == Level.State.Human)
        {
            if (type == Type.Confirm)
                Turn.SelectTarget(Turn.activeUnit.cell);
            else if (type == Type.Cancel)
                Turn.CancelAction();
            else if (Turn.SelectAction(action, index))
                UI.primaryUnit.SetCommandCard(transition);
            else
                invalid = true;
            Audio.PlayClip(invalid ? Audio.sounds.invalid : Audio.sounds.click);
        }
    }

}
