using System;
using UnityEngine;

/// <summary>
/// Acci�n en la que un tri�ngulo ataca
/// </summary>
public class PlasmaSpray : EnemyTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: el tri�ngulo se abre</item>
    /// <item><c>Fire</c>: el tri�ngulo dispara</item>
    /// <item><c>Damage</c>: el tri�ngulo se cierra, aplica el da�o y se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: termina el ataque</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Fire,
        Damage,
        End
    }

    /// <summary>
    /// Estado actual de la acci�n
    /// </summary>
    private State state;

    /// <summary>
    /// Sistema de part�culas usado durante el ataque
    /// </summary>
    private ParticleSystem spray;

    /// <summary>
    /// Da�o que hace esta acci�n
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acci�n <c>PlasmaSpray</c>
    /// </summary>
    /// <param name="triangle">El tri�ngulo que realiza la acci�n</param>
    /// <param name="spray">El sistema de part�culas que se usa durante el ataque</param>
    public PlasmaSpray(Triangle triangle, ParticleSystem spray) : base(triangle)
    {
        search = new NeighbourhoodSearch();
        damage = new Damage(11);
        this.spray = spray;
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
                spray.transform.rotation = Quaternion.LookRotation(targetUnit.transform.position - unit.transform.position);
                spray.Play();
                break;
            case State.Damage:
                state = State.End;
                unit.animator.SetTrigger("Attack");
                damage.Apply(targetUnit);
                UI.secondaryUnit.SetHealth();
                if (targetUnit.health == 0)
                {
                    Level.Kill(targetUnit);
                    Timeline.Update();
                }
                else
                    unit.actionController.StopAction();
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
