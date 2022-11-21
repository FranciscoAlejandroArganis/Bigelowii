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
    public Sphere sphere;

    /// <summary>
    /// Unidad plantilla usada para reclutar nuevos tetraedros
    /// </summary>
    public Tetrahedron tetrahedron;

    /// <summary>
    /// Unidad plantilla usada para reclutar nuevos octaedros
    /// </summary>
    public Octahedron octahedron;

    /// <summary>
    /// Unidad plantilla usada para reclutar nuevos icosaedros
    /// </summary>
    public Icosahedron icosahedron;

    public override void Start()
    {
        base.Start();
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        sphere = prefab.GetComponent<Sphere>();
    }

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
                    case 6:
                        commandButton.active = true;
                        commandButton.sprite = UI.sprites.cones[octahedron.cost - 1];
                        commandButton.image.sprite = UI.sprites.octahedron;
                        commandButton.action = new Recruit(this, octahedron);
                        commandButton.type = CommandButton.Type.Command;
                        commandButton.transition = 1;
                        break;
                    case 7:
                        commandButton.active = true;
                        commandButton.sprite = UI.sprites.cones[icosahedron.cost - 1];
                        commandButton.image.sprite = UI.sprites.icosahedron;
                        commandButton.action = new Recruit(this, icosahedron);
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

    public override bool Rotates()
    {
        return false;
    }

}
