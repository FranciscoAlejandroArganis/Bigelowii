/// <summary>
/// Agente que controla una unidad
/// </summary>
public abstract class Agent
{

    /// <summary>
    /// La unidad que controla el agente
    /// </summary>
    protected Unit unit;

    /// <summary>
    /// La celda a la que se va a mover este turno
    /// </summary>
    protected Cell destination;

    /// <summary>
    /// Construye un nuevo agente
    /// </summary>
    /// <param name="unit">La unidad que controlar� el nuevo agente</param>
    public Agent (Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// El agente presiona un bot�n en la tarjeta de comandos
    /// </summary>
    /// <param name="button">El �ndice del bot�n</param>
    public void PressButton(int button)
    {
        CommandButton commandButton = UI.primaryUnit.commandCard[button];
        Turn.SelectAction(commandButton.action, button);
        UI.primaryUnit.SetCommandCard(commandButton.transition);
    }

    /// <summary>
    /// Se asume que ya se ha encontrado un camino desde el agente hasta la celda destino
    /// <para>Actualiza el destino a la mejor celda en el camino que el agente puede alcanzar</para>
    /// </summary>
    protected void BestDestination()
    {
        uint maxDistance = unit.movement;
        while (destination.distance > maxDistance)
            destination = destination.predecesor;
    }

    /// <summary>
    /// Limpia las propiedades de las celdas despu�s de que se busc� un destino
    /// </summary>
    /// <param name="search">La b�squeda usada para buscar un destino</param>
    protected void ClearCells(Search search)
    {
        foreach (Cell cell in search.results)
        {
            cell.visited = false;
            cell.distance = 0;
            cell.predecesor = null;
        }
        search.results.Clear();
    }

    /// <summary>
    /// El agente toma su siguiente decisi�n
    /// </summary>
    public abstract void MakeDecision();

    /// <summary>
    /// Busca un destino y asigna <c>destination</c> a esa celda
    /// <para><c>destination</c> es <c>null</c> si no se encuentra una celda destino para el turno actual</para>
    /// </summary>
    protected abstract void SearchDestination();

}
