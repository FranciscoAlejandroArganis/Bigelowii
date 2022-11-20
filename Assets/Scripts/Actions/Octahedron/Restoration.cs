using System;

/// <summary>
/// Acción en la que un octaedro recupera la salud de otra unidad aliada
/// </summary>
public class Restoration : FriendlyTargetAction
{

    /// <summary>
    /// Regeneración de salud de esta acción
    /// </summary>
    private Heal heal;

    /// <summary>
    /// Construye una nueva acción <c>Restoration</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public Restoration(Unit unit) : base(unit)
    {
        search = new EuclideanDistanceSearch(3);
        heal = new Heal(50);
        heal.BehaviorModifiers(unit);
    }

    public override void Execute()
    {
        heal.Apply(targetUnit);
        UI.secondaryUnit.SetHealth();
        unit.actionController.StopAction();
    }

    protected override bool ValidTarget(Cell cell)
    {
        return cell != unit.cell && base.ValidTarget(cell) && cell.unit.health < cell.unit.maxHealth;
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
