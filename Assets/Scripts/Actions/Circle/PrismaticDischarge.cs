using System;
using System.Collections.Generic;
using UnityEngine;

public class PrismaticDischarge : UntargetedAction
{

    public static uint range = 3;

    public static uint baseDamage = 81;

    public enum State
    {
        Start,
        Fire,
        Damage,
        End
    }

    private State state;

    private Damage damage;

    private PrismaticDischargeVFX discharge;

    private List<Unit> targets;

    public PrismaticDischarge(Unit unit, PrismaticDischargeVFX discharge) : base(unit)
    {
        damage = new Damage(baseDamage);
        damage.BehaviorModifiers(unit);
        this.discharge = discharge;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Fire;
                unit.animator.SetTrigger("Attack");
                break;
            case State.Fire:
                state = State.Damage;
                FindTargets();
                discharge.points = new Vector3[targets.Count];
                int index = 0;
                while (index < targets.Count)
                {
                    discharge.points[index] = targets[index].transform.position;
                    index++;
                }
                discharge.Play();
                discharge.Timer(1, this);
                break;
            case State.Damage:
                state = State.End;
                unit.animator.SetTrigger("Attack");
                discharge.Stop();
                bool kills = false;
                foreach (Unit unit in targets)
                {
                    damage.Apply(unit);
                    if (unit.health == 0)
                    {
                        kills = true;
                        Level.Kill(unit, this.unit.player);
                    }
                }
                targets = null;
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

    private void FindTargets()
    {
        targets = new List<Unit>();
        foreach (Collider collider in Physics.OverlapSphere(unit.cell.transform.position, range, Utilities.mapLayer))
        {
            Cell cell = collider.GetComponent<Cell>();
            Unit unit = cell.unit;
            if (unit && unit.IsHostile(this.unit))
                targets.Add(unit);
        }
    }

}
