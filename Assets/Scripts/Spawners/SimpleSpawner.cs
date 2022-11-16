/// <summary>
/// Generador simple de unidades
/// <para>Cada cierto tiempo genera una cantidad fija de unidades</para>
/// </summary>
public class SimpleSpawner : Spawner
{

    /// <summary>
    /// Cantidad de unidades que se generan por cada activación del generador
    /// </summary>
    public uint amount;

    public override bool Spawn()
    {
        bool generated = false;
        uint index = 1;
        while (index <= amount)
        {
            Cell spawnPoint = GetSpawnLocation();
            if (spawnPoint)
            {
                Level.NewUnit(unit, player, spawnPoint);
                generated = true;
            }
            index++;
        }
        return generated;
    }

}
