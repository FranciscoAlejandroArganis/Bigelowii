using System;
using UnityEngine;

/// <summary>
/// Acci�n en la que una esfera recluta una nueva unidad
/// </summary>
public class Recruit : CellTargetAction
{

    /// <summary>
    /// Enumeraci�n de los estados de la acci�n
    /// <list type="bullet">
    /// <item><c>Start</c>: aparece la nueva unidad en el mapa</item>
    /// <item><c>Update</c>: se actualiza la l�nea de tiempo</item>
    /// <item><c>End</c>: la unidad termina su turno</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Update,
        End
    }

    /// <summary>
    /// Estado actual de la acci�n
    /// </summary>
    private State state;

    /// <summary>
    /// Plantilla de la unidad que aparecer�
    /// </summary>
    private Unit template;

    private VFXWrapper spawnEffect;

    /// <summary>
    /// Construye una nueva acci�n <c>Recruit</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    /// <param name="template">La unidad plantilla que se usar� para crear la nueva unidad</param>
    public Recruit(Unit unit, Unit template, VFXWrapper spawnEffect) : base(unit)
    {
        search = new EuclideanDistanceSearch(3);
        this.template = template;
        this.spawnEffect = spawnEffect;
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
                state = State.Update;
                Level.cones -= template.cost;
                UI.resources.UpdatePanel();
                Level.NewUnit(template, unit.player, targetCell);
                spawnEffect.transform.position = new Vector3(targetCell.transform.position.x, .5f, targetCell.transform.position.z);
                spawnEffect.Play();
                spawnEffect.Timer(2, this);
                break;
            case State.Update:
                state = State.End;
                spawnEffect.Stop();
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
