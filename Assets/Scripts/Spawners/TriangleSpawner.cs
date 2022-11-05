/// <summary>
/// Generador de tri�ngulos
/// <para>Asegura que siempre hay al menos tantos tri�ngulos en el mapa como el m�nimo</para>
/// <para>Si hay menos del m�nimo, genera tantos tri�ngulos como se requieran para alcanzarlo</para>
/// </summary>
public class TriangleSpawner : Spawner
{

    /// <summary>
    /// La cantidad m�nima de tri�ngulos que que deben existir en el mapa
    /// </summary>
    public uint minimum;

    /// <summary>
    /// La plantilla que se usa para generar nuevos tri�ngulos
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
