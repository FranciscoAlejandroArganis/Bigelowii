using UnityEngine;

/// <summary>
/// Agente inteligente que controla un círculo
/// </summary>
public class CircleAgent : Agent
{

    public enum State
    {
        Heal1,
        Heal2,
        Move1,
        Move2,
        Attack1,
        Attack2,
        Delayed1,
        Delayed2
    }

    public State state;

    private uint damageLastTurn;

    private Cell delayedTarget;

    public CircleAgent(Circle circle) : base(circle) { }

    public override void MakeDecision()
    {
        switch (state)
        {
            case State.Heal1:
                if (unit.health < unit.maxHealth)
                {
                    PressButton(2);
                    state = State.Heal2;
                }
                else
                    EnterMove2State();
                break;
            case State.Heal2:
                Turn.SelectTarget(unit.cell);
                state = State.Move1;
                break;
            case State.Move1:
                EnterMove2State();
                break;
            case State.Move2:
                Turn.SelectTarget(destination);
                state = State.Attack1;
                break;
            case State.Attack1:
                EnterAttack2State();
                break;
            case State.Attack2:
                Turn.SelectTarget(unit.cell);
                state = State.Delayed1;
                break;
            case State.Delayed1:
                EnterDelayed2State();
                break;
            case State.Delayed2:
                Turn.SelectTarget(delayedTarget ? delayedTarget : unit.cell);
                state = State.Heal1;
                destination = null;
                delayedTarget = null;
                break;
        }
    }

    private void EnterMove2State()
    {
        SearchDestination();
        if (destination)
        {
            if (destination == unit.cell)
                EnterAttack2State();
            else
            {
                PressButton(0);
                state = State.Move2;
            }
        }
        else
            EnterDelayed2State();
    }

    private void EnterAttack2State()
    {
        // damageLastTurn ya se actualizó al daño del turno actual
        if (damageLastTurn > 0)
        {
            PressButton(1);
            state = State.Attack2;
        }
        else
            EnterDelayed2State();
    }

    private void EnterDelayed2State()
    {
        SetDelayedTarget();
        PressButton(delayedTarget ? 3 : 15);
        state = State.Delayed2;
    }

    protected override void SearchDestination()
    {
        MaximumSearch search = new MaximumSearch(UnconstrainedDamage);
        search.FindCells(unit.cell);
        uint potentialDamage = search.value;
        if (potentialDamage > 0)
        {
            destination = search.maximum;
            ClearCells(search);
            search = new MaximumSearch(ConstrainedDamage);
            search.FindCells(unit.cell);
            if (potentialDamage > search.value && potentialDamage > search.value + damageLastTurn)
            {
                BestDestination();
                damageLastTurn = UnconstrainedDamage(destination);
            }
            else
            {
                destination = search.maximum;
                damageLastTurn = search.value;
            }
            ClearCells(search);
        }
    }

    /// <summary>
    /// Regresa la cantidad de daño que se haría al usar la acción <c>PrismaticDischarge</c> desde la celda especificada
    /// </summary>
    /// <param name="cell">La celda desde donde se usaría la acción <c>PrismaticDischarge</c></param>
    /// <returns>La cantidad total de daño aplicado a unidades enemigas al usar la acción <c>PrismaticDischarge</c></returns>
    private uint UnconstrainedDamage(Cell cell)
    {
        uint damage = 0;
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, PrismaticDischarge.range, Utilities.mapLayer))
        {
            Unit unit = collider.GetComponent<Cell>().unit;
            if (unit && unit.IsHostile(this.unit))
                damage += unit.health <= PrismaticDischarge.baseDamage ? unit.health : PrismaticDischarge.baseDamage;
        }
        return damage;
    }

    /// <summary>
    /// Regresa la cantidad de daño que se haría al usar la acción <c>PrismaticDischarge</c> desde una celda alcanzable en el turno actual
    /// </summary>
    /// <param name="cell">La celda desde donde se usaría la acción <c>PrismaticDischarge</c></param>
    /// <returns>La cantidad total de daño aplicado a unidades enemigas al usar la acción <c>PrismaticDischarge</c> o <c>0</c> si la celda no es alcanzable en el turno actual</returns>
    private uint ConstrainedDamage(Cell cell)
    {
        return cell.distance <= unit.movement ? UnconstrainedDamage(cell) : 0;
    }

    /// <summary>
    /// Asigna <c>delayedTarget</c> a la celda que el agente usará como objetivo para la acción retrasada <c>CallLightning</c>
    /// <para><c>delayedTarget</c> será <c>null</c> si ya no hay más unidades enemigas</para>
    /// </summary>
    private void SetDelayedTarget()
    {
        Event timelineEvent = Timeline.events.FindLast(EnemyUnitAwake);
        if (timelineEvent != null)
            delayedTarget = timelineEvent.action.unit.cell;
    }

    /// <summary>
    /// Predicado que determina si el evento especificado es el inicio de turno de una unidad enemiga
    /// </summary>
    /// <param name="timelineEvent">El evento que se prueba</param>
    /// <returns><c>true</c> si el evento es el inicio de turno de una unidad enemiga </returns>
    private bool EnemyUnitAwake(Event timelineEvent)
    {
        Action action = timelineEvent.action;
        return action is Awake && action.unit.IsHostile(unit) && !action.unit.cell.actionFlags.HasFlag(Cell.ActionFlags.CallLightning);
    }

}
