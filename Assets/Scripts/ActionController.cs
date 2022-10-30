using UnityEngine;

/// <summary>
/// Componente que controla las acciones de una unidad
/// </summary>
public class ActionController : MonoBehaviour
{

    /// <summary>
    /// Enumeraci�n de los estados de un unidad que realiza una acci�n
    /// <list type="bullet">
    /// <item><c>Idle</c>: la unidad no est� realizando ni est� esperando para realizar una acci�n</item>
    /// <item><c>Camera</c>: la unidad est� esperando a la c�mara para realizar una acci�n</item>
    /// <item><c>Rotation</c>: la unidad est� esperando rotar para realizar una acci�n</item>
    /// <item><c>Action</c>: la unidad est� realizando una acci�n</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Idle,
        Camera,
        Rotation,
        Action
    }

    /// <summary>
    /// Estado actual de la unidad que realiza una acci�n
    /// </summary>
    public State state;

    /// <summary>
    /// Acci�n que realiza actualmente la unidad
    /// </summary>
    public Action action;

    /// <summary>
    /// La unidad que realiza acciones con este componente
    /// </summary>
    private Unit unit;

    public void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Camera:
                if (CameraController.state == CameraController.State.Fixed)
                {
                    Cell target = action.GetTarget();
                    if (target)
                    {
                        state = State.Rotation;
                        unit.movementController.RotateTowards(target);
                    }
                    else EnterActionState();
                }
                break;
            case State.Rotation:
                if (unit.movementController.state == MovementController.State.Stationary) EnterActionState();
                break;
            case State.Action:
                break;
        }
    }

    /// <summary>
    /// Hace que la unidad empiece a realizar la acci�n especificada
    /// </summary>
    /// <param name="action">La acci�n que realizar� la unidad</param>
    public void StartAction(Action action)
    {
        Level.state = Level.State.Standby;
        unit.cell.highlight.Add(Highlight.State.Unit);
        if (action is TargetedAction)
            action.AddTargetHighlight(action.GetTarget());
        this.action = action;
        UI.timeline.action = action;
        state = State.Camera;
        CameraController.LookAt(unit.cell);
    }

    /// <summary>
    /// Hace que la unidad termine de realizar la acci�n
    /// </summary>
    public void StopAction()
    {
        unit.cell.highlight.Remove(Highlight.State.Unit);
        if (action is TargetedAction)
            action.RemoveTargetHighlight(action.GetTarget());
        if (unit == Turn.activeUnit)
        {
            Turn.SelectUnit(unit);
            Level.state = Level.State.Human;
        }
        else
            Level.state = Level.State.Event;
        action = null;
        state = State.Idle;
    }

    /// <summary>
    /// Se manda a llamar cuando la unidad tiene la orientaci�n correcta para inciar el efecto de la acci�n
    /// </summary>
    private void EnterActionState()
    {
        state = State.Action;
        action.Execute();
    }

}
