using UnityEngine;

/// <summary>
/// Tri�ngulo
/// </summary>
public class Triangle : Unit2D
{

    /// <summary>
    /// Sistema de part�culas que se usa durante la acci�n <c>PlasmaSpray</c>
    /// </summary>
    public ParticleSystem spray;

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

    /// <summary>
    /// Se manda a llamar cuando la animaci�n del tri�ngulo est� en el cuadro en el que debe disparar
    /// </summary>
    public void FirePlasmaSpray()
    {
        actionController.action.Execute();
    }

}
