/// <summary>
/// Generador de rect�ngulos
/// <para>La prpiedad es la cantidad de unidades que los jugadores objetivo han eliminado desde la �ltima vez que se generaron rect�ngulos</para>
/// <para>Cuando se supera el umbral, se genera una cantidad de rect�ngulos proporcional a las nuevas eliminaciones</para>
/// </summary>
public class RectangleSpawner : ThresholdSpawner
{

    /// <summary>
    /// Peso, en eliminaciones, de cada nuevo rect�ngulo generado
    /// <para>Al superar el umbral, se generan <c>currentKills / weight</c> nuevos rect�ngulos</para>
    /// </summary>
    public uint weight;

    /// <summary>
    /// Cantidad de unidades que hab�an eliminado los jugadores objetivo en el momento de la �ltima vez que se generaron rect�ngulos
    /// </summary>
    private uint previousKills;

    /// <summary>
    /// Cantidad total de unidades que han sido eliminadas por los jugadores objetivo desde el inicio del nivel
    /// </summary>
    private uint currentKills;

    protected override uint GetPropertyValue()
    {
        currentKills = 0;
        foreach (Player player in targets)
            currentKills += player.GetDictionaryTotal(player.unitsKilled);
        return currentKills - previousKills;
    }

    protected override uint GetUnitsToGenerate(uint property)
    {
        previousKills = currentKills;
        return currentKills / weight;
    }

}
