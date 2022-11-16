/// <summary>
/// Generador de rectángulos
/// <para>La prpiedad es la cantidad de unidades que los jugadores objetivo han eliminado desde la última vez que se generaron rectángulos</para>
/// <para>Cuando se supera el umbral, se genera una cantidad de rectángulos proporcional a las nuevas eliminaciones</para>
/// </summary>
public class RectangleSpawner : ThresholdSpawner
{

    /// <summary>
    /// Peso, en eliminaciones, de cada nuevo rectángulo generado
    /// <para>Al superar el umbral, se generan <c>currentKills / weight</c> nuevos rectángulos</para>
    /// </summary>
    public uint weight;

    /// <summary>
    /// Cantidad de unidades que habían eliminado los jugadores objetivo en el momento de la última vez que se generaron rectángulos
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
