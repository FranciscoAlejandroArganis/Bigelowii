/// <summary>
/// Generador de rectángulos
/// <para>Genera rectángulos cada vez que las unidades eliminadas de un grupo de jugadores objetivo superan un umbral</para>
/// <para>Cuando se supera el umbral, se genera una cantidad de rectángulos proporcional a las unidades eliminadas</para>
/// </summary>
public class RectangleSpawner : Spawner
{

    /// <summary>
    /// Umbral necesario para que se generen nuevos rectángulos
    /// <para>Cada vez que se supera, es necesario volver a acumular al menos esta cantidad de eliminaciones para que se vuelvan a generar rectángulos</para>
    /// </summary>
    public uint kills;

    /// <summary>
    /// Peso, en eliminaciones, de cada nuevo rectángulo generado
    /// <para>Al superar el umbral, si <c>currentKills</c> es la cantidad de eliminaciones que han acumulados los jugadores objetivo desde la última generación, se generan <c>currentKills</c>/<c>weight</c> nuevos rectángulos</para>
    /// </summary>
    public uint weight;

    /// <summary>
    /// Plantilla que se usa para generar nuevos rectángulos
    /// </summary>
    public Rectangle rectangle;

    /// <summary>
    /// Arreglo con los jugadores objetivo que acumulan eliminaciones para el generador
    /// </summary>
    public Player[] targets;

    /// <summary>
    /// Cantidad de unidades eliminadas que tenían acumuladas los jugadores objetivo la última vez que se generaron rectángulos
    /// </summary>
    private uint previousKills;

    public override bool Spawn()
    {
        uint currentKills = 0;
        foreach (Player player in targets)
            currentKills += player.GetDictionaryTotal(player.unitsKilled);
        if (currentKills - previousKills < kills)
            return false;
        previousKills = currentKills;
        currentKills /= weight;
        bool generated = false;
        while (currentKills > 0)
        {
            Cell spawnPoint = GetSpawnLocation();
            if (spawnPoint)
            {
                Level.NewUnit(rectangle, player, spawnPoint);
                generated = true;
            }
            currentKills--;
        }
        return generated;
    }

}
