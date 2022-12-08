using System;
using UnityEngine;

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

    private ParticleSystem call;

    private CallLightningVFX lightning;

    public CallLightningDelayed(Unit unit, Cell targetCell, ParticleSystem call, CallLightningVFX lightning) : base(unit)
    {
        damage = new Damage(102);
        damage.BehaviorModifiers(unit);
        this.targetCell = targetCell;
        this.call = call;
        this.lightning = lightning;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Impact;
                CameraController.LookAt(targetCell);
                lightning.positionStart = targetCell.transform.position + new Vector3(0, 8, 0);
                lightning.positionEnd = targetCell.transform.position + new Vector3(0, .5f, 0);
                lightning.Play();
                lightning.Timer(2, this);
                break;
            case State.Impact:
                state = State.End;
                lightning.Stop();
                OnEventDestroy();
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
        eventButton.image.sprite = UI.sprites.callLightning;
    }

    public override void OnEventDestroy()
    {
        targetCell.actionFlags &= ~Cell.ActionFlags.CallLightning;
        GameObject.Destroy(call.gameObject);
    }

}
