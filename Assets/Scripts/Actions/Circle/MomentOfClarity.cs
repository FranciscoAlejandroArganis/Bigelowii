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
                momentOfClarity = GameObject.Instantiate(momentOfClarity, unit.transform.position - new Vector3(0, .24609375f, 0), Quaternion.identity);
                momentOfClarity.unit = unit;
                momentOfClarity.Play();
                break;
            case State.End:
                GameObject.Destroy(momentOfClarity.gameObject);
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
