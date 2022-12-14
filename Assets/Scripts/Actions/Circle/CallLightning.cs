using System;
using UnityEngine;

public class CallLightning : CellTargetAction
{

    public enum State
    {
        Start,
        End
    }

    private static int delay = 6;

    /// <summary>
    /// Estado actual de la acci?n
    /// </summary>
    private State state;

    private ParticleSystem call;

    private CallLightningVFX lightning;

    public CallLightning(Unit unit, ParticleSystem call, CallLightningVFX lightning) : base(unit)
    {
        search = new FullMapSearch();
        this.call = call;
        this.lightning = lightning;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                targetCell.actionFlags |= Cell.ActionFlags.CallLightning;
                ParticleSystem call = GameObject.Instantiate(this.call, new Vector3(targetCell.transform.position.x, .75f, targetCell.transform.position.z), Quaternion.identity);
                CallLightningDelayed delayed = new CallLightningDelayed(unit, targetCell, call, lightning);
                Timeline.EnqueueLast(new Event(delayed, unit.delay < delay ? unit.delay : delay));
                Awake awake = new Awake(unit);
                Timeline.EnqueueLast(new Event(awake, unit.delay));
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                Turn.activeUnit = null;
                unit.actionsTaken = Level.TechnologyMask(unit);
                unit.actionController.StopAction();
                break;
        }
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

    protected override bool ValidTarget(Cell cell)
    {
        return !cell.actionFlags.HasFlag(Cell.ActionFlags.CallLightning);
    }

}
