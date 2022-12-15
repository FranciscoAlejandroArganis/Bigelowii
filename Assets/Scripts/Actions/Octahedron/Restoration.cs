using System;
using UnityEngine;

/// <summary>
/// Acci�n en la que un octaedro recupera la salud de otra unidad aliada
/// </summary>
public class Restoration : FriendlyTargetAction
{

    public enum State
    {
        Start,
        End
    }

    private State state;

    /// <summary>
    /// Regeneraci�n de salud de esta acci�n
    /// </summary>
    private Heal heal;

    private VFXWrapper restoration;

    /// <summary>
    /// Construye una nueva acci�n <c>Restoration</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    public Restoration(Unit unit, VFXWrapper restoration) : base(unit)
    {
        search = new EuclideanDistanceSearch(3);
        heal = new Heal(50);
        heal.BehaviorModifiers(unit);
        this.restoration = restoration;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                restoration.transform.position = targetUnit.transform.position + new Vector3(0, .00390625f, 0);
                restoration.Play();
                restoration.Timer(2, this);
                break;
            case State.End:
                restoration.Stop();
                heal.Apply(targetUnit);
                UI.secondaryUnit.SetHealth();
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        return cell != unit.cell && base.ValidTarget(cell) && cell.unit.health < cell.unit.maxHealth;
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
