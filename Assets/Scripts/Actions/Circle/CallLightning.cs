using System;

public class CallLightning : CellTargetAction
{

    public enum State
    {
        Start,
        End
    }

    private static int delay = 12;

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    public CallLightning(Unit unit) : base(unit)
    {
        search = new FullMapSearch();
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                CallLightningDelayed delayed = new CallLightningDelayed(unit, targetCell);
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
        return true;
    }

}
