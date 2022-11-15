using System;

/// <summary>
/// Acci�n en la que un octaedro recupera la salud de otra unidad aliada
/// </summary>
public class Restoration : FriendlyTargetAction
{

    /// <summary>
    /// Regeneraci�n de salud de esta acci�n
    /// </summary>
    private Heal heal;

    /// <summary>
    /// Construye una nueva acci�n <c>Restoration</c>
    /// </summary>
    /// <param name="octahedron">El octaedro que realiza la acci�n</param>
    public Restoration(Octahedron octahedron) : base(octahedron)
    {
        search = new EuclideanDistanceSearch(3);
        heal = new Heal(50);
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
