using UnityEngine;

/// <summary>
/// Tetraedro
/// <para>Unidad de combate básica</para>
/// </summary>
public class Tetrahedron : Unit3D
{

    /// <summary>
    /// Componente que renderiza el haz usado durante la acción <c>ParticleBeam</c>
    /// </summary>
    public LineRenderer beam;

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.tetrahedron;
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
                    case 1:
                        commandButton.image.sprite = UI.sprites.attack;
                        commandButton.action = new ParticleBeam(this, beam);
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
