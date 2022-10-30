using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

/// <summary>
/// Unidad
/// </summary>
public abstract class Unit : MonoBehaviour
{

    /// <summary>
    /// La salud actual de la unidad
    /// </summary>
    public uint health;

    /// <summary>
    /// La salud máxima de la unidad
    /// </summary>
    public uint maxHealth;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar entre cada turno
    /// </summary>
    public int delay;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar antes de su primer turno
    /// </summary>
    public int initialDelay;

    /// <summary>
    /// Máxima distancia que la unidad se puede mover en un turno
    /// </summary>
    public uint movement;

    /// <summary>
    /// La celda sobre la que se encuentra la unidad
    /// </summary>
    public Cell cell;

    /// <summary>
    /// Identificador del jugador que controla la unidad
    /// </summary>
    public uint player;

    /// <summary>
    /// Identificador del equipo al que pertence la unidad
    /// </summary>
    public uint team;

    /// <summary>
    /// Controlador del movimiento de la unidad
    /// </summary>
    public MovementController movementController;

    /// <summary>
    /// Controlador de las acciones de la unidad
    /// </summary>
    public ActionController actionController;

    public void Start()
    {
        movementController = GetComponent<MovementController>();
        actionController = GetComponent<ActionController>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, Utilities.mapLayer))
        {
            cell = hit.collider.GetComponent<Cell>();
            cell.unit = this;
        }
    }

    /// <summary>
    /// Regresa el sprite de la unidad
    /// </summary>
    /// <returns>El sprite de la unidad</returns>
    public abstract Sprite GetUnitSprite();

    /// <summary>
    /// Asigna las propiedades del botón especificado en la tarjeta de comandos
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    /// <param name="card">El índice de la tarjeta</param>
    /// <param name="button">El índice del botón</param>
    public abstract void SetCommandButton(CommandButton commandButton, uint card, uint button);

    /// <summary>
    /// Asigna las propiedades de un botón vacío
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetEmptyButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.empty;
        commandButton.action = null;
        commandButton.type = CommandButton.Type.Empty;
        commandButton.transition = 0;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de movimiento
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetMoveButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.move;
        commandButton.action = new Move(this);
        commandButton.type = CommandButton.Type.Command;
        commandButton.transition = 1;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de terminar turno
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetEndTurnButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.endTurn;
        commandButton.action = new Sleep(this);
        commandButton.type = CommandButton.Type.Command;
        commandButton.transition = 1;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de cancelar
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetCancelButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.cancel;
        commandButton.action = null;
        commandButton.type = CommandButton.Type.Cancel;
        commandButton.transition = 0;
    }

}
