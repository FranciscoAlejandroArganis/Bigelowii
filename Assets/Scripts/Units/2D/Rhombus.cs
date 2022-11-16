using UnityEngine;

/// <summary>
/// Rombo
/// <para>Unidad de soporte que aumenta el daño de otras unidades</para>
/// </summary>
public class Rhombus : Unit2D
{

    /// <summary>
    /// Plantilla usada para generar sistemas de partículas durante la acción <c>DiamondBlessing</c>
    /// </summary>
    public Particle blessing;

    /// <summary>
    /// Sistema de partículas usado durante la acción <c>DiamondBlessing</c>
    /// </summary>
    public ParticleSystem blessedVisual;

    public override void Start()
    {
        base.Start();
        agent = new RhombusAgent(this);
    }

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.rhombus;
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
                    case 2:
                        commandButton.image.sprite = UI.sprites.empty;
                        commandButton.action = new DiamondBlessing(this, blessing, blessedVisual);
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
