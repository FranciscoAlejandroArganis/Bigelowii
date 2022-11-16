using UnityEngine;

/// <summary>
/// Agente inteligente que controla un rombo
/// <para>La acción distinguida es <c>DiamondBlessing</c></para>
/// <para>Toma como objetivo la unidad aliada con mayor salud que no esté bendecida</para>
/// </summary>
public class RhombusAgent : BasicAgent
{

    /// <summary>
    /// Indica si el agente puede bendecir a la unidad obejtivo en el turno actual
    /// </summary>
    private bool canBless;

    /// <summary>
    /// Construye un nuevo agente de un rombo
    /// </summary>
    /// <param name="rhombus">El rombo que controlará el agente</param>
    public RhombusAgent(Rhombus rhombus) : base(rhombus, 2) { }

    protected override void EnterSleep2State()
    {
        targetUnit = null;
        base.EnterSleep2State();
    }

    protected override void SearchDestination()
    {
        MaximumSearch maximumSearch = new MaximumSearch(InRangeFriendlyUnitHealth);
        maximumSearch.FindCells(unit.cell);
        if (maximumSearch.value > 0)
        {
            destination = maximumSearch.maximum;
            foreach (Collider collider in Physics.OverlapSphere(destination.transform.position, DiamondBlessing.range, Utilities.mapLayer))
            {
                Unit unit = collider.GetComponent<Cell>().unit;
                if (unit && unit.IsFriendly(this.unit) && Blessed.CanBeBlessed(unit) && unit.health == maximumSearch.value)
                {
                    targetUnit = unit;
                    break;
                }
            }
            BestDestination();
            canBless = false;
            foreach (Collider collider in Physics.OverlapSphere(destination.transform.position, DiamondBlessing.range, Utilities.mapLayer))
            {
                Unit unit = collider.GetComponent<Cell>().unit;
                if (unit == targetUnit)
                    canBless = true;
            }
        }
        ClearCells(maximumSearch);
    }

    protected override bool ActionCanBeUsed()
    {
        return canBless;
    }

    /// <summary>
    /// Regresa la máxima salud que tiene una unidad aliada en rango de <c>DiamondBlessing</c> desde la celda
    /// </summary>
    /// <param name="cell">La celda que se evalúa</param>
    /// <returns>La máxima salud de una unidad aliada en rango de <c>DiammondBlessing</c> desde <c>cell</c> o <c>0</c> si no hay una unidad aliada que se pueda bendecir</returns>
    private uint InRangeFriendlyUnitHealth(Cell cell)
    {
        uint maxHealth = 0;
        foreach (Collider collider in Physics.OverlapSphere(cell.transform.position, DiamondBlessing.range, Utilities.mapLayer))
        {
            Unit unit = collider.GetComponent<Cell>().unit;
            if (unit && unit.IsFriendly(this.unit) && Blessed.CanBeBlessed(unit) && unit.health > maxHealth)
                maxHealth = unit.health;
        }
        return maxHealth;
    }

}
