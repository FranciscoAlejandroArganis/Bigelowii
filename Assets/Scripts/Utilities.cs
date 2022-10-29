using UnityEngine;

/// <summary>
/// Contiene atributos y métodos estáticos auxiliares
/// </summary>
public class Utilities
{

    /// <summary>
    /// Tau
    /// </summary>
    public const float TAU = 6.283185f;

    /// <summary>
    /// La máscara de las celdas del mapa
    /// </summary>
    public static LayerMask mapLayer = LayerMask.GetMask("Map");

    /// <summary>
    /// Arreglo con las raíces sextas unitarias
    /// </summary>
    public static Vector3[] roots =
    {
        new Vector3(1, 0, 0),
        new Vector3(.5f, 0, .8660254f),
        new Vector3(-.5f, 0, .8660254f),
        new Vector3(-1, 0, 0),
        new Vector3(-.5f, 0, -.8660254f),
        new Vector3(.5f, 0, -.8660254f)
    };

    /// <summary>
    /// Arreglo con las rotaciones correspondientes a las raíces sextas unitarias
    /// </summary>
    public static Quaternion[] rotations =
    {
        new Quaternion(0, .7071068f, 0, .7071068f),
        new Quaternion(0, .2588191f, 0, .9659259f),
        new Quaternion(0, -.2588191f, 0, .9659259f),
        new Quaternion(0, -.7071068f, 0, .7071068f),
        new Quaternion(0, .9659259f, 0, -.2588191f),
        new Quaternion(0, .9659259f, 0, .2588191f)
    };

}
