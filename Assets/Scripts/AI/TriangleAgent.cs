/// <summary>
/// Guión del agente inteligente que controla un triángulo
/// <para>Se mueve lo más que puede hacia su presa</para>
/// <para>Si no tiene una presa o su presa es inalcanzable, elige una nueva presa (la unidad enemiga más cercana)</para>
/// <para>Si no puede encontrar una presa, entonces termina su turno</para>
/// <para>Si llega hasta su presa, la ataca 1 vez</para>
/// <para>Después termina su turno</para>
/// </summary>
public class TriangleAgent : Agent
{

    /// <summary>
    /// Enumeración de los estados del agente
    /// <list type="bullet">
    /// <item><c>Move1</c>: elige la acción moverse</item>
    /// <item><c>Move2</c>: confirma el destino del movimiento</item>
    /// <item><c>Attack1</c>: elige la acción atacar</item>
    /// <item><c>Attack2</c>: confirma el objetivo del ataque</item>
    /// <item><c>Sleep1</c>: elige la acción terminar turno</item>
    /// <item><c>Sleep2</c>: confirma la terminación de su turno</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Move1,
        Move2,
        Attack1,
        Attack2,
        Sleep1,
        Sleep2
    }

    /// <summary>
    /// Estado actual del agente
    /// </summary>
    private State state;

    /// <summary>
    /// La unidad que el triángulo persigue y ataca
    /// </summary>
    private Unit prey;

    /// <summary>
    /// Búsqueda para encontrar una presa
    /// </summary>
    private FirstMatchSearch search;

    /// <summary>
    /// Construye un nuevo agente de un triángulo
    /// </summary>
    /// <param name="unit">El triángulo que controlará el agente</param>
    public TriangleAgent(Unit unit) : base(unit) { }

    public override void MakeDecision()
    {
        switch (state)
        {
            case State.Move1:
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
                    EnterSleep2State();
                break;
            case State.Move2:
                Turn.SelectTarget(destination);
                state = State.Attack1;
                break;
            case State.Attack1:
                if (AdjacentToPrey(unit.cell))
                    EnterAttack2State();
                else
                    EnterSleep2State();
                break;
            case State.Attack2:
                Turn.SelectTarget(prey.cell);
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
    /// Entra al estado <c>Attack2</c>
    /// </summary>
    private void EnterAttack2State()
    {
        PressButton(1);
        state = State.Attack2;
    }

    /// <summary>
    /// Entra al estado <c>Sleep2</c>
    /// </summary>
    private void EnterSleep2State()
    {
        PressButton(15);
        state = State.Sleep2;
    }

    /// <summary>
    /// Regresa <c>true</c> cuando la celda es adyacente a la presa
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda es adyacente a la celda de la presa</returns>
    private bool AdjacentToPrey(Cell cell)
    {
        return cell.EdgeIndex(prey.cell) < 6;
    }

    /// <summary>
    /// Regresa <c>true</c> cuando la celda es adyacente a una unidad enemiga
    /// <para>Asigna la unidad como la nueva presa</para>
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda es adyacente a la celda de una unidad enemiga</returns>
    private bool AdjacentToEnemyUnit(Cell cell)
    {
        foreach (Cell neighbor in cell.neighbors)
        {
            if (neighbor && neighbor.unit && neighbor.unit.IsHostile(unit))
            {
                prey = neighbor.unit;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Busca un destino y asigna <c>destination</c> a esa celda
    /// <para><c>destination</c> es <c>null</c> si no se encunentra una celda destino para el turno actual</para>
    /// </summary>
    private void SearchDestination()
    {   
        if (prey)
        {
            search = new FirstMatchSearch(AdjacentToPrey);
            search.FindCells(unit.cell);
            if (search.firstMatch)
            {
                destination = search.firstMatch;
                BestDestination();
            }
            else
                prey = null;
            ClearCells();
        }
        if (!destination)
        {
            search = new FirstMatchSearch(AdjacentToEnemyUnit);
            search.FindCells(unit.cell);
            if (search.firstMatch)
            {
                destination = search.firstMatch;
                BestDestination();
            }
            ClearCells();
        }
    }

    /// <summary>
    /// Limpia las propiedades de las celdas después de que se buscó un destino
    /// </summary>
    private void ClearCells()
    {
        foreach (Cell cell in search.results)
        {
            cell.visited = false;
            cell.distance = 0;
            cell.predecesor = null;
        }
        search.results.Clear();
    }

}
