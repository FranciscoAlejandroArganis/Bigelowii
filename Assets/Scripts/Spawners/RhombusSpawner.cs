using UnityEngine;
/// <summary>
/// Generador de rombos
/// <para>La propiedad es la diferencia de unidades de combate menos rombos que los jugadores objetivo controlan actualmente en el mapa</para>
/// </summary>
public class RhombusSpawner : ThresholdSpawner
{

    protected override uint GetPropertyValue()
    {
        uint rhombi = 0;
        uint combatUnits = 0;
        foreach (Player player in targets)
        {
            foreach (Unit unit in player.GetComponentsInChildren<Unit>())
            {
                if (unit is Rhombus)
                    rhombi++;
                else if (!(unit is Sphere))
                    combatUnits++;
            }
        }
        return rhombi < combatUnits ? combatUnits - rhombi : 0;
    }

    protected override uint GetUnitsToGenerate(uint property)
    {
        return property + 1 >> 1;
    }

}
