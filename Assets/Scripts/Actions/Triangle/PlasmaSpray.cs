using System;
using UnityEngine;

public class PlasmaSpray : EnemyTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el triángulo inicia la animación de atacar</item>
    /// <item><c>Fire</c>: el triángulo dispara y se actualiza la línea de tiempo</item>
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
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Sistema de partículas que se usa durante el ataque
    /// </summary>
    private ParticleSystem spray;

    /// <summary>
    /// Daño que hace esta acción
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acción <c>PlasmaSpray</c>
    /// </summary>
    /// <param name="unit">El triángulo que realiza la acción</param>
    /// <param name="spray">El sistema de partículas que se usa durante el ataque</param>
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
