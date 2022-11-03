using UnityEngine;

/// <summary>
/// Componente que controla los movimientos de una unidad
/// </summary>
public class MovementController : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados del movimiento de una unidad
    /// <list type="bullet">
    /// <item><c>Stationary</c>: la unidad no tiene movimiento</item>
    /// <item><c>Rotation</c>: la unidad rota en su lugar</item>
    /// <item><c>Linear</c>: la unidad se desplaza siguiendo una línea recta</item>
    /// <item><c>Quadratic</c>: la unidad se desplaza siguiendo una parábola</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Stationary,
        Rotation,
        Linear,
        Quadratic
    }

    /// <summary>
    /// Estado actual del movimiento de la unidad
    /// </summary>
    public State state;

    /// <summary>
    /// Velocidad con la que se desplaza la unidad
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// Velocidad con la que rota la unidad
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// La unidad que se mueve con este componente
    /// </summary>
    private Unit unit;

    /// <summary>
    /// El camino actual a través del cual se mueve la unidad
    /// </summary>
    private Cell[] path;

    /// <summary>
    /// Índice en <c>path</c> de la celda actual a través de la cual se mueve la unidad
    /// </summary>
    private uint index;

    /// <summary>
    /// Índice de la dirección que tendrá la unidad al salir de la celda actual
    /// </summary>
    private uint nextDirection;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    private float time;
    
    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qué tan rápido se completa el cambio</para>
    /// </summary>
    private float speed;

    /// <summary>
    /// Posición inicial de la unidad antes del cambio
    /// </summary>
    private Vector3 positionStart;

    /// <summary>
    /// Posición final de la unidad después del cambio
    /// </summary>
    private Vector3 positionEnd;

    /// <summary>
    /// Posición del centro de la celda a través de la cual se mueve la unidad
    /// </summary>
    private Vector3 center;

    /// <summary>
    /// Rotación inicial de la unidad antes del cambio
    /// </summary>
    private Quaternion rotationStart;

    /// <summary>
    /// Rotación final de la unidad después del cambio
    /// </summary>
    private Quaternion rotationEnd;

    public void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void Update()
    {
        switch (state)
        {
            case State.Stationary:
                break;
            case State.Rotation:
                transform.rotation = Quaternion.Slerp(rotationStart, rotationEnd, time);
                time += speed * Time.deltaTime;
                if (time >= 1)
                {
                    transform.rotation = rotationEnd;
                    state = State.Stationary;
                }
                break;
            case State.Linear:
                transform.position = Vector3.Lerp(positionStart, positionEnd, time);
                time += speed * Time.deltaTime;
                if (time >= 1)
                    ExitLinearState();
                break;
            case State.Quadratic:
                transform.position = center + (1 - time) * (1 - time) * (positionStart - center) + time * time * (positionEnd - center);
                if (unit is Unit3D)
                    transform.rotation = Quaternion.LookRotation((1 - time) * (center - positionStart) + time * (positionEnd - center));
                time += speed * Time.deltaTime;
                if (time >= 1)
                    ExitQuadraticState();
                break;
        }
    }

    /// <summary>
    /// Hace que la unidad rote hacia la celda especificada
    /// </summary>
    /// <param name="cell">La celda que la unidad va estar mirando</param>
    public void RotateTowards(Cell cell)
    {
        if (unit is Unit3D)
        {
            Vector3 position = cell.UnitPosition(unit); // La posición hacia donde debe mirar
            if (position != transform.position)
            {
                rotationEnd = Quaternion.LookRotation(position - transform.position);
                if (rotationEnd != transform.rotation)
                {
                    rotationStart = transform.rotation;
                    speed = rotationSpeed / Quaternion.Angle(rotationStart, rotationEnd);
                    time = 0;
                    state = State.Rotation;
                }
            }
        }
    }

    /// <summary>
    /// Hace que la unidad se mueva por el camino especificado
    /// </summary>
    /// <param name="path">El arreglo de celdas que definen el camino por donde se moverá la unidad</param>
    public void MoveThrough(Cell[] path)
    {
        this.path = path;
        index = 0;
        nextDirection = path[0].EdgeIndex(path[1]);
        // Empieza el movimiento lineal a través de la primera celda del camino
        positionEnd = path[0].UnitPosition(unit) + .5f * Utilities.roots[nextDirection];
        speed = 2 * movementSpeed;
        EnterLinearState();
    }

    /// <summary>
    /// Entra al estado de movimiento lineal
    /// <para><c>positionEnd</c> y <c>speed</c> ya están asignados</para>
    /// </summary>
    private void EnterLinearState()
    {
        positionStart = transform.position;
        time = 0;
        state = State.Linear;
    }

    /// <summary>
    /// Sale del estado de movimiento lineal
    /// </summary>
    private void ExitLinearState()
    {
        transform.position = positionEnd;
        index++;
        if (index == path.Length)
        {
            // Ha terminado el recorrido del camino
            path[0].unit = null;
            path[0].highlight.Remove(Highlight.State.Unit);
            unit.cell = path[index - 1];
            path[index - 1].unit = unit;
            path[index - 1].highlight.Add(Highlight.State.Unit);
            path = null;
            state = State.Stationary;
            unit.actionController.action.Execute();
        }
        else MoveThroughNextCell();
    }

    /// <summary>
    /// Entra al estado de movimiento cuadrático
    /// <para><c>rotationEnd</c> y <c>nextDirection</c> ya están asignados</para>
    /// </summary>
    private void EnterQuadraticState()
    {
        positionStart = transform.position;
        center = path[index].UnitPosition(unit);
        positionEnd = center + .5f * Utilities.roots[nextDirection];
        speed = movementSpeed;
        time = 0;
        state = State.Quadratic;
    }

    /// <summary>
    /// Sale del estado de movimiento cuadráctico
    /// </summary>
    private void ExitQuadraticState()
    {
        transform.position = positionEnd;
        transform.rotation = rotationEnd;
        index++;
        MoveThroughNextCell();
    }

    /// <summary>
    /// Empieza el movimiento a través de la siguiente celda en el camino
    /// </summary>
    private void MoveThroughNextCell()
    {
        // Sale de path[index-1] y entra a path[index]
        if (index + 1 == path.Length)
        {
            // Empieza el movimiento lineal a través de la última celda del camino
            positionEnd = path[index].UnitPosition(unit);
            speed = 2 * movementSpeed;
            EnterLinearState();
        }
        else
        {
            // Empieza el movimiento a través de una celda intermendia en el camino
            nextDirection = path[index].EdgeIndex(path[index + 1]);
            rotationEnd = Utilities.rotations[nextDirection];
            if (rotationEnd == transform.rotation)
            {
                positionEnd = path[index].UnitPosition(unit) + .5f * Utilities.roots[nextDirection];
                speed = movementSpeed;
                EnterLinearState();
            }
            else EnterQuadraticState();
        }
    }

}
