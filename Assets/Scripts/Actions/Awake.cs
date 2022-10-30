/// <summary>
/// Acción en la que una unidad empieza su nuevo turno
/// </summary>
public class Awake : UntargetedAction
{

    /// <summary>
    /// Construye una nueva acción de iniciar turno
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public Awake(Unit unit) : base(unit) { }

    public override void Execute()
    {
        TurnHandler.activeUnit = unit;
        unit.actionController.StopAction();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        eventButton.image.sprite = unit.GetUnitSprite();
    }
}
