using System;

/// <summary>
/// Acción en la que un octaedro recupera la salud de otra unidad aliada
/// </summary>
public class Restoration : FriendlyTargetAction
{

    public enum State
    {
        Start,
        End
    }

    private State state;

    /// <summary>
    /// Regeneración de salud de esta acción
    /// </summary>
    private Heal heal;

    private RestorationVFX restoration;

    /// <summary>
    /// Construye una nueva acción <c>Restoration</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public Restoration(Unit unit, RestorationVFX restoration) : base(unit)
    {
        search = new EuclideanDistanceSearch(3);
        heal = new Heal(50);
        heal.BehaviorModifiers(unit);
        this.restoration = restoration;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                restoration.transform.position = targetUnit.transform.position;
                restoration.Play();
                restoration.Timer(3, this);
                break;
            case State.End:
                restoration.Stop();
                heal.Apply(targetUnit);
                UI.secondaryUnit.SetHealth();
                unit.actionController.StopAction();
                break;
        }
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
