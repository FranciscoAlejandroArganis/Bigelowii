using System.ComponentModel;
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
        Confirm,
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

    /// <summary>
    /// Índice del botón dentro de la tarjeta de comandos
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
