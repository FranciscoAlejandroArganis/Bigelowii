using System;
using UnityEngine;

public class PrismaticDischarge : UntargetedAction
{

    public static uint range = 3;

    public static uint baseDamage = 81;

    public enum State
    {
        Start,
        End
    }

    private State state;

    private Damage damage;

    public PrismaticDischarge(Unit unit) : base(unit)
    {
        damage = new Damage(baseDamage);
        damage.BehaviorModifiers(unit);
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                bool kills = false;
                foreach (Collider collider in Physics.OverlapSphere(unit.cell.transform.position, range, Utilities.mapLayer))
                {
                    Cell cell = collider.GetComponent<Cell>();
                    Unit unit = cell.unit;
                    if (unit && unit.IsHostile(this.unit))
                    {
                        damage.Apply(unit);
                        if (unit.health == 0)
                        {
                            kills = true;
                            Level.Kill(unit, this.unit.player);
                        }
                    }
                }
                if (kills)
                    Timeline.Update();
                else
                    unit.actionController.StopAction();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    public override void AddTargetHighlight(Cell cell)
    {
        cell.highlight.Add(Highlight.State.SelectedTarget);
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 3, Utilities.mapLayer))
            collider.GetComponent<Cell>().highlight.Add(Highlight.State.AreaOfEffect);
    }

    public override void RemoveTargetHighlight(Cell cell)
    {
        cell.highlight.Remove(Highlight.State.SelectedTarget);
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 3, Utilities.mapLayer))
            collider.GetComponent<Cell>().highlight.Remove(Highlight.State.AreaOfEffect);
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }
}
