using System;

/// <summary>
/// Acción en la que una esfera recluta una nueva unidad
/// </summary>
public class Recruit : CellTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: se actualiza la línea de tiempo</item>
    /// <item><c>End</c>: la unidad termina su turno</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        End
    }

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Plantilla de la unidad que aparecerá
    /// </summary>
    private Unit template;

    /// <summary>
    /// Construye una nueva acción <c>Recruit</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    /// <param name="template">La unidad plantilla que se usará para crear la nueva unidad</param>
    public Recruit(Unit unit, Unit template) : base(unit)
    {
        search = new EuclideanDistanceSearch(3);
        this.template = template;
    }

    public override bool Validate()
    {
        return Level.cones >= template.cost;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.End;
                Level.cones -= template.cost;
                UI.resources.UpdatePanel();
                Level.NewUnit(template, unit.player, targetCell);
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        return cell.IsFree() && !cell.actionFlags.HasFlag(Cell.ActionFlags.Recruit);
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }

}
