using UnityEngine;

/// <summary>
/// Proyectil que se mueve hacia un destino como resultado de una acci�n
/// </summary>
public abstract class Projectile : MonoBehaviour
{

    /// <summary>
    /// Acci�n que gener� el proyectil
    /// </summary>
    public Action action;

    /// <summary>
    /// Posici�n inicial desde donde parte el proyectil
    /// </summary>
    public Vector3 positionStart;

    /// <summary>
    /// Posici�n final del destino que alcanzar� el proyectil
    /// </summary>
    public Vector3 positionEnd;

    /// <summary>
    /// Velocidad del proyectil
    /// </summary>
    public float speed;

    /// <summary>
    /// Tiempo actual del movimiento del proyectil
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    protected float time;

    /// <summary>
    /// Se manda a llamar cuando el proyectil alcanza su destino
    /// </summary>
    protected virtual void Impact()
    {
        transform.position = positionEnd;
        action.Execute();
    }

}
