using System;

/// <summary>
/// Acción en la que un rectángulo ataca
/// </summary>
public class DarkMirage : EnemyTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el rectángulo se estira</item>
    /// <item><c>Fire</c>: el rectángulo dispara</item>
    /// <item><c>Damage</c>: el rectángulo se aplasta, aplica el daño y se actualiza la línea de tiempo</item>
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
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Sistema de partículas usado durante el ataque
    /// </summary>
    private ParticleSystemWrapper mirage;

    /// <summary>
    /// Daño que hace esta acción
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acción <c>DarkMirage</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    /// <param name="mirage">El sistema de partículas que se usa durante el ataque</param>
    public DarkMirage(Unit unit, ParticleSystemWrapper mirage) : base(unit)
    {
        search = new NeighbourhoodSearch();
        damage = new Damage(47);
        damage.BehaviorModifiers(unit);
        this.mirage = mirage;
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
                mirage.transform.position = targetUnit.transform.position;
                mirage.Play();
                break;
            case State.Damage:
                state = State.End;
                unit.animator.SetTrigger("Attack");
                damage.Apply(targetUnit);
                UI.secondaryUnit.SetHealth();
                if (targetUnit.health == 0)
                {
                    Level.Kill(targetUnit, unit.player);
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
