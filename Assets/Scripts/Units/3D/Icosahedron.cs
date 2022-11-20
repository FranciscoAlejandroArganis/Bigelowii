using UnityEngine;

/// <summary>
/// Icosaedro
/// <para>Unidad de combate lenta pero con daño de área</para>
/// </summary>
public class Icosahedron : Unit3D
{

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.icosahedron;
    }

    public override void SetCommandButton(CommandButton commandButton, uint card, int button)
    {
        commandButton.active = false;
        switch (card)
        {
            case 0:
                switch (button)
                {
                    case 0:
                        SetMoveButton(commandButton);
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
            case 2:
                switch (button)
                {
                    case 14:
                        SetConfirmButton(commandButton);
                        break;
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
