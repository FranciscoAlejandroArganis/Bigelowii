using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jugador al que pertencen unidades
/// <para>Puede ser humano o controlado por la IA</para>
/// </summary>
public class Player : MonoBehaviour
{

    /// <summary>
    /// Identificador del equipo al que pertence el jugador
    /// </summary>
    public uint team;

    /// <summary>
    /// Diccionario con la información de las unidades que ha eliminado el jugador
    /// <para>Mapea el sprite de un tipo de unidad a la cantidad de unidades eliminadas de ese tipo</para>
    /// </summary>
    public Dictionary<Sprite, uint> unitsKilled;

    /// <summary>
    /// Diccionario con la información de las unidades que ha perdido el jugador
    /// <para>Mapea el sprite de un tipo de unidad la cantidad de unidades perdidas de ese tipo</para>
    /// </summary>
    public Dictionary<Sprite, uint> unitsLost;

    public void Start()
    {
        unitsKilled = new Dictionary<Sprite, uint>();
        unitsLost = new Dictionary<Sprite, uint>();
    }

    /// <summary>
    /// Registra la información de la unidad en el diccionario especificado
    /// <para>Aumenta la cantidad del tipo de unidad en el diccionario</para>
    /// </summary>
    /// <param name="unit">La unidad que se registra</param>
    /// <param name="dictionary">El diccionario en el que se registra la información de la unidad</param>
    public void AddToDictionary(Unit unit, Dictionary<Sprite, uint> dictionary)
    {
        Sprite sprite = unit.GetUnitSprite();
        if (dictionary.ContainsKey(sprite))
            dictionary[sprite]++;
        else
            dictionary[sprite] = 1;
    }

    /// <summary>
    /// Regresa la cantidad total de unidades que se han registrado en el diccionario especificado
    /// </summary>
    /// <param name="dictionary">El diccionario con la información de las unidades registradas</param>
    /// <returns></returns>
    public uint GetDictionaryTotal(Dictionary<Sprite, uint> dictionary)
    {
        uint sum = 0;
        foreach (KeyValuePair<Sprite, uint> pair in dictionary)
            sum += pair.Value;
        return sum;
    }

}
