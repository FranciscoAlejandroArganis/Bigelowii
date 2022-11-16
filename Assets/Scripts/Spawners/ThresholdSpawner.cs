/// <summary>
/// Generador de unidades por umbral
/// <para>Genera unidades cada vez que una propiedad de un grupo de jugadores objetivo es igual o mayor a un valor umbral</para>
/// </summary>
public abstract class ThresholdSpawner : Spawner
{

    /// <summary>
    /// Umbral necesario para que se generen unidades
    /// </summary>
    public uint threshold;

    /// <summary>
    /// Arreglo con los jugadores objetivo que determinan el valor de la propiedad
    /// </summary>
    public Player[] targets;

    public override bool Spawn()
    {
        uint property = GetPropertyValue();
        if (property < threshold)
            return false;
        property = GetUnitsToGenerate(property);
        bool generated = false;
        while (property > 0)
        {
            Cell spawnPoint = GetSpawnLocation();
            if (spawnPoint)
            {
                Level.NewUnit(unit, player, spawnPoint);
                generated = true;
            }
            property--;
        }
        return generated;
    }

    /// <summary>
    /// Regresa el valor de la propiedad de los jugadores objetivo
    /// </summary>
    /// <returns>El valor actual de la propiedad</returns>
    protected abstract uint GetPropertyValue();

    /// <summary>
    /// Recibe el valor de la propiedad y regresa la cantidad de unidades a generar
    /// <para>Solo se manda a llamar si el valor de la propiedad ha superado el umbral</para>
    /// </summary>
    /// <param name="property">El valor actual de la propiedad</param>
    /// <returns>La cantidad de nuevas unidades que se intentarán generar</returns>
    protected abstract uint GetUnitsToGenerate(uint property);

}
