/// <summary>
/// Generador de triángulos
/// <para>Asegura que siempre hay al menos tantos triángulos en el mapa como el mínimo</para>
/// <para>Si hay menos del mínimo, genera tantos triángulos como se requieran para alcanzarlo</para>
/// </summary>
public class TriangleSpawner : Spawner
{

    /// <summary>
    /// La cantidad mínima de triángulos que que deben existir en el mapa
    /// </summary>
    public uint minimum;

    /// <summary>
    /// La plantilla que se usa para generar nuevos triángulos
    /// </summary>
    public Triangle triangle;

    public override bool Spawn()
    {
        uint counter = 0;
        foreach (Unit unit in player.GetComponentsInChildren<Unit>())
        {
            if (unit is Triangle)
                counter += 1;
        }
        if (minimum <= counter)
            return false;
        bool generated = false;
        counter = minimum - counter;
        while (counter > 0)
        {
            Cell spawnPoint = GetSpawnLocation();
            if (spawnPoint)
            {
                Level.NewUnit(triangle, player, spawnPoint);
                generated = true;
            }
            counter--;
        }
        return generated;
    }

}
