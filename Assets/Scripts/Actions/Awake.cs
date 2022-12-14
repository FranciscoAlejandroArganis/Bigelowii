using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acci?n en la que una unidad empieza su nuevo turno
/// </summary>
public class Awake : UntargetedAction
{

    /// <summary>
    /// Construye una nueva acci?n de iniciar turno
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci?n</param>
    public Awake(Unit unit) : base(unit) { }

    public override void Execute()
    {
        Turn.activeUnit = unit;
        Agent agent = unit.agent;
        if (agent != null)
        {
            AI.agent = agent;
            AI.state = AI.State.Dormant;
        }
        else if (unit is Sphere)
        {
            foreach (Cell neighbor in unit.cell.neighbors)
            {
                if (neighbor)
                {
                    List<Cone> cones = neighbor.cones;
                    if (cones.Count > 0)
                    {
                        Audio.PlayClip(Audio.sounds.collectCone);
                        Cone cone = cones[0];
                        cones.RemoveAt(0);
                        Level.cones++;
                        UI.resources.UpdatePanel();
                        GameObject.Destroy(cone.gameObject);
                        break;
                    }
                }
            }
        }
        unit.actionController.StopAction();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        eventButton.image.sprite = unit.GetUnitSprite();
    }

}
