/// <summary>
/// Generador de rombos
/// <para>La propiedad es la cantidad total de unidades que los jugadores objetivo controlan actualmente en el mapa</para>
/// </summary>
public class RhombusSpawner : ThresholdSpawner
{

    protected override uint GetPropertyValue()
    {
        uint units = 0;
        foreach (Player player in targets)
            units += (uint)transform.childCount;
        return units;
    }

    protected override uint GetUnitsToGenerate(uint property)
    {
        return property + 1 >> 1;
    }

}
