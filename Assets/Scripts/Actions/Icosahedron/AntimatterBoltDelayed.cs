using System;
using UnityEngine;

/// <summary>
/// Acción en la que un icosaedro ataca
/// </summary>
public class AntimatterBoltDelayed : CellTargetAction
{

    /// <summary>
    /// Enumeración de los estados de la acción
    /// <list type="bullet">
    /// <item><c>Start</c>: el icosaedro dispara</item>
    /// <item><c>Impact</c>: el proyectil impacta</item>
    /// <item><c>Damage</c>: se aplica el daño y se actualiza la línea de tiempo</item>
    /// <item><c>End</c>: termina el ataque</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Start,
        Impact,
        Damage,
        End
    }

    /// <summary>
    /// Estado actual de la acción
    /// </summary>
    private State state;

    /// <summary>
    /// Arreglo con el daño que se aplica en cada zona del área de efecto
    /// </summary>
    private Damage[] splashDamage;

    /// <summary>
    /// Proyectil usado durante el ataque
    /// </summary>
    private ParabolicProjectile bolt;

    private AntimatterBoltImpactVFX impact;

    /// <summary>
    /// Construye una nueva acción <c>AntimatterBoltDelayed</c>
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    /// <param name="targetCell">La celda objetivo de la acción</param>
    /// <param name="bolt">La plantilla usada para generar el proyectil del ataque</param>
    public AntimatterBoltDelayed(Unit unit, Cell targetCell, ParabolicProjectile bolt, AntimatterBoltImpactVFX impact) : base(unit)
    {
        splashDamage = new Damage[] { new Damage(52), new Damage(17), new Damage(5) };
        foreach (Damage damage in splashDamage)
            damage.BehaviorModifiers(unit);
        this.targetCell = targetCell;
        this.bolt = bolt;
        this.impact = impact;
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Start:
                state = State.Impact;
                bolt = GameObject.Instantiate(bolt, unit.transform.position, Quaternion.identity);
                bolt.positionStart = bolt.transform.position;
                bolt.positionEnd = targetCell.UnitPosition(unit);
                bolt.action = this;
                CameraController.FollowProjectile(bolt);
                break;
            case State.Impact:
                state = State.Damage;
                GameObject.Destroy(bolt.gameObject);
                impact = GameObject.Instantiate(impact, bolt.positionEnd, Quaternion.identity);
                impact.Play();
                impact.Timer(1, this);
                break;
            case State.Damage:
                state = State.End;
                impact.Stop();
                foreach (Collider collider in Physics.OverlapSphere(targetCell.transform.position, 2, Utilities.mapLayer))
                {
                    Cell cell = collider.GetComponent<Cell>();
                    Unit unit = cell.unit;
                    if (unit && unit.IsHostile(this.unit))
                    {
                        float distance = Vector3.Distance(targetCell.transform.position, cell.transform.position);
                        if (distance < 1)
                            splashDamage[0].Apply(unit);
                        else if (distance < 2)
                            splashDamage[1].Apply(unit);
                        else
                            splashDamage[2].Apply(unit);
                        if (unit.health == 0)
                            Level.Kill(unit, this.unit.player);
                    }
                }
                Timeline.Dequeue();
                Timeline.Update();
                break;
            case State.End:
                GameObject.Destroy(impact.gameObject);
                unit.actionController.StopAction();
                break;
        }
    }

    public override void AddTargetHighlight(Cell cell)
    {
        cell.highlight.Add(Highlight.State.SelectedTarget);
        if (unit.actionController.action != this)
        {
            foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 2, Utilities.mapLayer))
                collider.GetComponent<Cell>().highlight.Add(Highlight.State.AreaOfEffect);
        }
    }

    public override void RemoveTargetHighlight(Cell cell)
    {
        cell.highlight.Remove(Highlight.State.SelectedTarget);
        if (unit.actionController.action != this)
        {
            foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, 2, Utilities.mapLayer))
                collider.GetComponent<Cell>().highlight.Remove(Highlight.State.AreaOfEffect);
        }
    }

    protected override bool ValidTarget(Cell cell)
    {
        throw new NotImplementedException();
    }

    public override void SetEventButton(EventButton eventButton)
    {
        eventButton.image.sprite = UI.sprites.antimatterBolt;
    }

}
