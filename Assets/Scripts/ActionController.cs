using UnityEngine;

/// <summary>
/// Componente que controla las acciones de una unidad
/// </summary>
public class ActionController : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados de un unidad que realiza una acción
    /// <list type="bullet">
    /// <item><c>Idle</c>: la unidad no está realizando ni está esperando para realizar una acción</item>
    /// <item><c>Camera</c>: la unidad está esperando a la cámara para realizar una acción</item>
    /// <item><c>Rotation</c>: la unidad está esperando rotar para realizar una acción</item>
    /// <item><c>Action</c>: la unidad está realizando una acción</item>
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
    /// Estado actual de la unidad que realiza una acción
    /// </summary>
    public State state;

    /// <summary>
    /// Acción que realiza actualmente la unidad
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
    /// Hace que la unidad empiece a realizar la acción especificada
    /// </summary>
    /// <param name="action">La acción que realizará la unidad</param>
    public void StartAction(Action action)
    {
        Level.state = Level.State.Standby;
        this.action = action;
        unit.cell.highlight.Add(Highlight.State.Unit);
        UI.primaryUnit.unit = unit;
        UI.primaryUnit.Show();
        if (action is TargetedAction)
        {
            Cell target = action.GetTarget();
            action.AddTargetHighlight(target);
            if (action is UnitTargetAction)
            {
                UI.secondaryUnit.unit = target.unit;
                UI.secondaryUnit.Show();
            }
        }
        UI.timeline.action = action;
        state = State.Camera;
        CameraController.LookAt(unit.cell);
    }

    /// <summary>
    /// Hace que la unidad termine de realizar la acción
    /// </summary>
    public void StopAction()
    {
        unit.cell.highlight.Remove(Highlight.State.Unit);
        if (action is TargetedAction)
        {
            Cell target = action.GetTarget();
            action.RemoveTargetHighlight(target);
            if (action is UnitTargetAction)
                UI.secondaryUnit.Hide();
        }
        if (unit == Turn.activeUnit)
        {
            Turn.SelectUnit(unit);
            Level.state = unit.agent == null ? Level.State.Human : Level.State.AI;
        }
        else
        {
            UI.primaryUnit.Hide();
            Level.state = Level.State.Spawning;
        }
        action = null;
        state = State.Idle;
    }

    /// <summary>
    /// Se manda a llamar cuando la animación de muerte de la unidad está en el último cuadro
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Se manda a llamar cuando la unidad tiene la orientación correcta para inciar el efecto de la acción
    /// </summary>
    private void EnterActionState()
    {
        state = State.Action;
        action.Execute();
    }

}
