/// <summary>
/// Componente encargado de manejar un turno
/// </summary>
public class TurnHandler
{

    /// <summary>
    /// Enumeraci�n de los estados de un turno
    /// <list type="bullet">
    /// <item><c>Unit</c>: se est� seleccionando una unidad</item>
    /// <item><c>Action</c>: se est� seleccionando una acci�n</item>
    /// <item><c>Target</c>: se est� seleccionando un objetivo</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Unit,
        Action,
        Target
    }

    /// <summary>
    /// Estado actual del turno
    /// </summary>
    public static State state;

    /// <summary>
    /// La unidad de la que es el turno actual
    /// </summary>
    public static Unit activeUnit;

    /// <summary>
    /// La unidad seleccionada
    /// </summary>
    public static Unit selectedUnit;

}
