using System;
using UnityEngine;

/// <summary>
/// Acción en la que un rombo otorga un comportamiento <c>Blessed</c> a otra unidad aliada por 12 unidades de tiempo
/// <para>El comportamiento <c>Blessed</c> aumenta el daño de la unidad</para>
/// </summary>
public class DiamondBlessing : FriendlyTargetAction
{

    /// <summary>
    /// Rango de la acción
    /// </summary>
    public static uint range = 4;

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el rombo rota</item>
    /// <item><c>Blessing</c>: el rombo aplica la bendición y se actualiza la línea de tiempo</item>
    /// <item><c>End</c>: termina la acción</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Blessing,
        End
    }

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Sistema de partículas usado durante la acción
    /// </summary>
    private Particle blessing;

    /// <summary>
    /// Plantilla usada para generar el efecto visual del comportamiento <c>Blessed</c>
    /// </summary>
    private ParticleSystem blessedVisual;

    /// <summary>
    /// Construye una nueva acción <c>DiamondBlessed</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    /// <param name="blessing">El sistema de partículas que se usa durante la acción</param>
    /// <param name="blessedVisual">La plantilla del efecto visual del comportamiento <c>Blessed</c></param>
    public DiamondBlessing(Unit unit, Particle blessing, ParticleSystem blessedVisual) : base(unit)
    {
        search = new EuclideanDistanceSearch(range);
        this.blessing = blessing;
        this.blessedVisual = blessedVisual;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Blessing;
                unit.animator.SetTrigger("Blessing");
                blessing.Play();
                break;
            case State.Blessing:
                state = State.End;
                unit.animator.SetTrigger("Blessing");
                Blessed blessed = new Blessed(targetUnit, blessedVisual);
                targetUnit.behaviors.Add(blessed);
                blessed.OnAdd();
                Event timelineEvent = new Event(new DiamondUnblessing(blessed), 12);
                Timeline.EnqueueFirst(timelineEvent);
                Timeline.Update();
                break;
            case State.End:
                unit.actionController.StopAction();
                break;
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        return base.ValidTarget(cell) && Blessed.CanBeBlessed(cell.unit);
    }

    public override void SetEventButton(EventButton eventButton)
    {
        throw new NotImplementedException();
    }
}
