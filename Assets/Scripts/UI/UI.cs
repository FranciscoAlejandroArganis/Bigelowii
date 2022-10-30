using UnityEngine;

/// <summary>
/// Componente encargado de manejar la interfaz de usario
/// </summary>
public class UI : MonoBehaviour
{

    /// <summary>
    /// Panel de la unidad primaria
    /// </summary>
    public static UnitPanel primaryUnit;

    /// <summary>
    /// Panel de la unidad secundaria
    /// </summary>
    public static UnitPanel secondaryUnit;

    /// <summary>
    /// Conjunto de sprites
    /// </summary>
    public static SpriteSet sprites;

    public void Start()
    {
        UnitPanel[] units = GetComponentsInChildren<UnitPanel>();
        primaryUnit = units[0];
        secondaryUnit = units[1];
        sprites = GetComponent<SpriteSet>();
    }

}
