using System;

public class CallLightningDelayed : CellTargetAction
{

    public enum State
    {
        Start,
        Impact,
        End
    }

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    private Damage damage;

    public CallLightningDelayed(Unit unit, Cell targetCell) : base(unit)
    {
        damage = new Damage(102);
        damage.BehaviorModifiers(unit);
        this.targetCell = targetCell;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                targetCell.actionFlags &= ~Cell.ActionFlags.CallLightning;
                Unit targetUnit = targetCell.unit;
                if (targetUnit && targetUnit.IsHostile(unit))
                {
                    damage.Apply(targetUnit);
                    if (targetUnit.health == 0)
                        Level.Kill(targetUnit, unit.player);
                }
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        throw new NotImplementedException();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        eventButton.image.sprite = UI.sprites.attack;
    }

    public override void OnEventDestroy()
    {
        targetCell.actionFlags &= ~Cell.ActionFlags.CallLightning;
    }

}
