using System;
using UnityEngine;

public class MomentOfClarity : UntargetedAction
{

    public enum State
    {
        Start,
        Cast,
        End
    }

    private State state;

    private ParticleSystemWrapper momentOfClarity;

    private Heal heal;

    public MomentOfClarity(Unit unit, ParticleSystemWrapper momentOfClarity) : base(unit)
    {
        heal = new Heal(173);
        heal.BehaviorModifiers(unit);
        this.momentOfClarity = momentOfClarity;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Cast;
                unit.animator.SetTrigger("Heal");
                break;
            case State.Cast:
                state = State.End;
                momentOfClarity.transform.rotation = Quaternion.identity;
                momentOfClarity.Play();
                break;
            case State.End:
                unit.animator.SetTrigger("Heal");
                heal.Apply(unit);
                unit.actionController.StopAction();
                break;
        }
        
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
