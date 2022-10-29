using UnityEngine;

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
    /// La salud m�xima de la unidad
    /// </summary>
    public uint maxHealth;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar entre cada turno
    /// </summary>
    public uint delay;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar antes de su primer turno
    /// </summary>
    public uint initialDelay;

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

    public void Start()
    {
        movementController = GetComponent<MovementController>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, Utilities.mapLayer))
        {
            cell = hit.collider.GetComponent<Cell>();
            cell.unit = this;
        }
    }

}
