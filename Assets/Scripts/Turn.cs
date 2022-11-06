/// <summary>
/// Componente encargado de manejar un turno
/// </summary>
public class Turn
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

    /// <summary>
    /// La acci�n seleccionada
    /// </summary>
    public static Action action;

    /// <summary>
    /// �ndice del bot�n de la acci�n seleccionada
    /// </summary>
    public static int button;

    /// <summary>
    /// Selecciona la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad que se selecciona</param>
    public static void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        unit.cell.highlight.Add(Highlight.State.Unit);
        UI.primaryUnit.unit = unit;
        UI.primaryUnit.Show();
        state = State.Action;
    }

    /// <summary>
    /// Quita la selecci�n de la unidad actualmente seleccionada
    /// </summary>
    public static void DeselectUnit()
    {
        selectedUnit.cell.highlight.Remove(Highlight.State.Unit);
        selectedUnit = null;
        UI.primaryUnit.Hide();
        state = State.Unit;
    }

    /// <summary>
    /// Valida que la acci�n especificada se pueda realizar y la selecciona
    /// </summary>
    /// <param name="action">La acci�n que se selecciona</param>
    /// <returns><c>true</c> si la acci�n se seleccion� porque es posible realizarla</returns>
    public static bool SelectAction(Action action, int button)
    {
        if (action.Validate() && action.SearchTargets())
        {
            Turn.button = button;
            Turn.action = action;
            state = State.Target;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Quita la selecci�n de la acci�n actualmente seleccionada
    /// </summary>
    public static void CancelAction()
    {
        UI.primaryUnit.SetCommandCard(0);
        action.ClearTargets();
        action = null;
        state = State.Action;
    }

    /// <summary>
    /// Selecciona el objetivo especificado
    /// </summary>
    /// <param name="target">La celda objetivo de la acci�n</param>
    public static void SelectTarget(Cell target)
    {
        activeUnit.actionsTaken |= 1 << button;
        action.SetTarget(target);
        UI.primaryUnit.SetCommandCard(0);
        action.ClearTargets();
        action.RemoveTargetHighlight(target);
        activeUnit.actionController.StartAction(action);
        action = null;
    }

}
