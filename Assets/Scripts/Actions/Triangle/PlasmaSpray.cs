using System;
using UnityEngine;

public class PlasmaSpray : EnemyTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: el tri�ngulo inicia la animaci�n de atacar</item>
    /// <item><c>Fire</c>: el tri�ngulo dispara y se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: termina el ataque</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Fire,
        End
    }

    /// <summary>
    /// Estado actual de la acci�n
    /// </summary>
    private State state;

    /// <summary>
    /// Sistema de part�culas que se usa durante el ataque
    /// </summary>
    private ParticleSystem spray;

    /// <summary>
    /// Da�o que hace esta acci�n
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acci�n <c>PlasmaSpray</c>
    /// </summary>
    /// <param name="unit">El tri�ngulo que realiza la acci�n</param>
    /// <param name="spray">El sistema de part�culas que se usa durante el ataque</param>
    public PlasmaSpray(Unit unit, ParticleSystem spray) : base(unit)
    {
        search = new NeighbourhoodSearch();
        this.spray = spray;
        damage = new Damage(1);
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
                state = State.End;
                spray.transform.SetPositionAndRotation(unit.transform.position, Quaternion.LookRotation(targetUnit.transform.position - unit.transform.position));
                spray.Play();
                damage.Apply(targetUnit);
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
