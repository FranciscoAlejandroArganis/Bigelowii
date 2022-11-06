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
    /// <param name="unit">La unidad que controlará el nuevo agente</param>
    public Agent (Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// El agente toma su siguiente decisión
    /// </summary>
    public abstract void MakeDecision();

    /// <summary>
    /// El agente presiona un botón en la tarjeta de comandos
    /// </summary>
    /// <param name="button">El índice del botón</param>
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

}
