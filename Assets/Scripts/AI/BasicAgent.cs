/// <summary>
/// Agente inteligente que se mueve, realiza una acci�n distinguida que toma una unidad como objetivo y termina su turno
/// <para>Se mueve lo m�s que puede hacia el objetivo de la acci�n distinguida</para>
/// <para>Si no tiene un objetivo o es inalcanzable, elige uno nuevo</para>
/// <para>Si no puede encontrar un objetivo para la acci�n distinguida, entonces termina su turno</para>
/// <para>Si alcanza su objetivo, realiza la acci�n distinguida 1 vez</para>
/// <para>Despu�s termina su turno</para>
/// </summary>
public abstract class BasicAgent : Agent
{

    /// <summary>
    /// Enumeraci�n de los estados del agente
    /// <list type="bullet">
    /// <item><c>Move1</c>: elige la acci�n moverse</item>
    /// <item><c>Move2</c>: confirma el destino del movimiento</item>
    /// <item><c>Attack1</c>: elige la acci�n distinguida</item>
    /// <item><c>Attack2</c>: confirma el objetivo de la acci�n distinguida</item>
    /// <item><c>Sleep1</c>: elige la acci�n terminar turno</item>
    /// <item><c>Sleep2</c>: confirma la terminaci�n de su turno</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Move1,
        Move2,
        Action1,
        Action2,
        Sleep1,
        Sleep2
    }

    /// <summary>
    /// Estado actual del agente
    /// </summary>
    public State state;

    /// <summary>
    /// El �ndice del bot�n de la acci�n distinguida
    /// </summary>
    protected int button;

    /// <summary>
    /// Unidad objetivo de la acci�n distinguida
    /// </summary>
    protected Unit targetUnit;

    /// <summary>
    /// Construye un nuevo agente b�sico
    /// </summary>
    /// <param name="unit">La unidad que controlar� el agente</param>
    /// <param name="button">El �ndice del bot�n de la acci�n distinguida</param>
    public BasicAgent(Unit unit, int button) : base(unit)
    {
        this.button = button;
    }

    public override void MakeDecision()
    {
        switch (state)
        {
            case State.Move1:
                SearchDestination();
                if (destination)
                {
                    if (destination == unit.cell)
                        EnterAction2State();
                    else
                    {
                        PressButton(0);
                        state = State.Move2;
                    }
                }
                else
                    EnterSleep2State();
                break;
            case State.Move2:
                Turn.SelectTarget(destination);
                state = State.Action1;
                break;
            case State.Action1:
                if (ActionCanBeUsed())
                    EnterAction2State();
                else
                    EnterSleep2State();
                break;
            case State.Action2:
                Turn.SelectTarget(targetUnit.cell);
                state = State.Sleep1;
                break;
            case State.Sleep1:
                EnterSleep2State();
                break;
            case State.Sleep2:
                Turn.SelectTarget(unit.cell);
                state = State.Move1;
                destination = null;
                break;
        }
    }

    /// <summary>
    /// Entra al estado <c>Action2</c>
    /// </summary>
    protected virtual void EnterAction2State()
    {
        PressButton(button);
        state = State.Action2;
    }

    /// <summary>
    /// Entra al estado <c>Sleep2</c>
    /// </summary>
    protected virtual void EnterSleep2State()
    {
        PressButton(15);
        state = State.Sleep2;
    }

    /// <summary>
    /// Determina si la acci�n distinguida se puede usar en el turno actual
    /// </summary>
    /// <returns><c>true</c> si la acci�n distinguida se puede usar en el turno actual</returns>
    protected abstract bool ActionCanBeUsed();

}
