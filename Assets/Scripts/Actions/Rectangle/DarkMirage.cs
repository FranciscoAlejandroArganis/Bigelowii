using System;

/// <summary>
/// Acci�n en la que un rect�ngulo ataca
/// </summary>
public class DarkMirage : EnemyTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: el rect�ngulo se estira</item>
    /// <item><c>Fire</c>: el rect�ngulo dispara</item>
    /// <item><c>Damage</c>: el rect�ngulo se aplasta, aplica el da�o y se actualiza la l�nea de tiempo</item>
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
    private ParticleSystemWrapper mirage;

    /// <summary>
    /// Da�o que hace esta acci�n
    /// </summary>
    private Damage damage;

    /// <summary>
    /// Construye una nueva acci�n <c>DarkMirage</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    /// <param name="mirage">El sistema de part�culas que se usa durante el ataque</param>
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
