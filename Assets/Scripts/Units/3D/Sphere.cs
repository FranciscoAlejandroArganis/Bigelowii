using UnityEngine;

/// <summary>
/// Esfera
/// <para>Recolecta recursos y recluta nuevas unidades</para>
/// </summary>
public class Sphere : Unit3D
{

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.empty;
    }

    public override void SetCommandButton(CommandButton commandButton, uint card, uint button)
    {
        switch (card)
        {
            case 0:
                switch (button)
                {
                    case 0:
                        SetMoveButton(commandButton);
                        break;
                    case 1:
                        commandButton.image.sprite = UI.sprites.empty;
                        commandButton.action = new Recruit(this, this);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 15:
                        SetEndTurnButton(commandButton);
                        break;
                    default:
                        SetEmptyButton(commandButton);
                        break;
                }
                break;
            case 1:
                switch (button)
                {
                    case 15:
                        SetCancelButton(commandButton);
                        break;
                    default:
                        SetEmptyButton(commandButton);
                        break;
                }
                break;
        }
    }

}
