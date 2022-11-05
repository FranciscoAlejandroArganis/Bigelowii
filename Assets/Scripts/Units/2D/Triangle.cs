using UnityEngine;

/// <summary>
/// Triángulo
/// <para>Unidad rápida que ataca en grupos</para>
/// </summary>
public class Triangle : Unit2D
{

    /// <summary>
    /// Sistema de partículas usado durante la acción <c>PlasmaSpray</c>
    /// </summary>
    public ParticleSystem spray;

    public override void Start()
    {
        base.Start();
        agent = new TriangleAgent(this);
    }

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.triangle;
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
                        commandButton.image.sprite = UI.sprites.attack;
                        commandButton.action = new PlasmaSpray(this, spray);
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

    public void OnParticleSystemStopped()
    {
        actionController.action.Execute();
    }

}
