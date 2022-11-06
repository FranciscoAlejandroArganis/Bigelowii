using System;
using UnityEngine;

/// <summary>
/// Acci�n en la que un tetraedro ataca
/// </summary>
public class ParticleBeam : EnemyTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: el tetraedro dispara</item>
    /// <item><c>Damage</c>: el tetraedro deja de disparar, aplica el da�o y se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: termina el ataque</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Damage,
        End
    }

    /// <summary>
    /// Estado actual de la acci�n
    /// </summary>
    private State state;

    /// <summary>
    /// Componente que renderiza el haz usado durante el ataque
    /// </summary>
    private LineRenderer beam;

    /// <summary>
    /// Da�o que hace esta acci�n
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acci�n <c>ParticleBeam</c>
    /// </summary>
    /// <param name="tetrahedron">El tetraedro que realiza la acci�n</param>
    public ParticleBeam(Tetrahedron tetrahedron, LineRenderer beam) : base(tetrahedron)
    {
        search = new NeighbourhoodSearch();
        damage = new Damage(35);
        this.beam = beam;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Damage;
                beam.SetPosition(1, Vector3.forward);
                unit.animator.SetTrigger("Attack");
                break;
            case State.Damage:
                state = State.End;
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
