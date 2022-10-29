using UnityEngine;

/// <summary>
/// Componente encargado de manejar un turno
/// </summary>
public class TurnHandler : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados de un turno
    /// <list type="bullet">
    /// <item><c>Unit</c>: se está seleccionando una unidad</item>
    /// <item><c>Action</c>: se está seleccionando una acción</item>
    /// <item><c>Target</c>: se está seleccionando un objetivo</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Unit,
        Action,
        Target
    }

    /// <summary>
    /// El estado actual del manejador de turnos
    /// </summary>
    public static State state;

}
