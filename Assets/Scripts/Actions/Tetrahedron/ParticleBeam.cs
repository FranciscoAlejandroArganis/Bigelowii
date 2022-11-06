using System;
using UnityEngine;

/// <summary>
/// Acción en la que un tetraedro ataca
/// </summary>
public class ParticleBeam : EnemyTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el tetraedro dispara</item>
    /// <item><c>Damage</c>: el tetraedro deja de disparar, aplica el daño y se actualiza la línea de tiempo</item>
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
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Componente que renderiza el haz usado durante el ataque
    /// </summary>
    private LineRenderer beam;

    /// <summary>
    /// Daño que hace esta acción
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acción <c>ParticleBeam</c>
    /// </summary>
    /// <param name="tetrahedron">El tetraedro que realiza la acción</param>
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
