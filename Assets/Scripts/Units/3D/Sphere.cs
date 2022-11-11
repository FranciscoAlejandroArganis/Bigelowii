using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esfera
/// <para>Recolecta recursos y recluta nuevas unidades</para>
/// </summary>
public class Sphere : Unit3D
{

    /// <summary>
    /// Unidad plantilla usada para reclutar nuevas esferas
    /// </summary>
    public Unit sphere;

    /// <summary>
    /// Unidad plantilla usada para reclutar nuevos tetraedros
    /// </summary>
    public Unit tetrahedron;

    public override Sprite GetUnitSprite()
    {
        return UI.sprites.sphere;
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
                    case 4:
                        commandButton.active = true;
                        commandButton.sprite = UI.sprites.cones[sphere.cost - 1];
                        commandButton.image.sprite = UI.sprites.sphere;
                        commandButton.action = new Recruit(this, sphere);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 5:
                        commandButton.active = true;
                        commandButton.sprite = UI.sprites.cones[tetrahedron.cost - 1];
                        commandButton.image.sprite = UI.sprites.tetrahedron;
                        commandButton.action = new Recruit(this, tetrahedron);
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

    public override bool Rotates()
    {
        return false;
    }

}
