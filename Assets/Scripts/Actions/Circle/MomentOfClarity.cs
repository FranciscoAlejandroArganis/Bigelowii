using System;

public class MomentOfClarity : UntargetedAction
{

    private Heal heal;

    public MomentOfClarity(Unit unit) : base(unit)
    {
        heal = new Heal(173);
        heal.BehaviorModifiers(unit);
    }

    public override void Execute()
    {
        heal.Apply(unit);
        unit.actionController.StopAction();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
