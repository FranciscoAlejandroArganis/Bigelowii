using UnityEngine;

/// <summary>
/// Comportamiento otorgado por la acción <c>DiamondBlessing</c>
/// <para>Duplica el daño de la unidad</para>
/// </summary>
public class Blessed : Behavior
{

    /// <summary>
    /// Determina si la unidad especificada puede ser bendecida
    /// </summary>
    /// <param name="unit">La unidad que se prueba</param>
    /// <returns><c>true</c> si la unidad puede adquirir un comportamiento <c>Blessed</c></returns>
    public static bool CanBeBlessed(Unit unit)
    {
        return !(unit is Rhombus || unit is Sphere || unit.behaviors.Exists(Is<Blessed>));
    }

    /// <summary>
    /// Sistema de partículas del efecto visual del comportamiento
    /// </summary>
    private ParticleSystem blessedVisual;

    /// <summary>
    /// Construye un nuevo comportamiento <c>Blessed</c> para la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad que tendrá el comportamiento</param>
    /// <param name="blessedVisual">La plantilla usada para generar el efecto visual del comportamiento</param>
    public Blessed(Unit unit, ParticleSystem blessedVisual) : base(unit)
    {
        this.blessedVisual = blessedVisual;
    }

    public override void OnAdd()
    {
        blessedVisual = GameObject.Instantiate(blessedVisual, unit.transform.position, Quaternion.identity, unit.transform);
        blessedVisual.Play();
    }

    public override void OnRemove()
    {
        GameObject.Destroy(blessedVisual.gameObject);
    }

    public override void Modify(Effect effect)
    {
        if (effect is Damage)
            (effect as Damage).damage <<= 1;
    }

}
