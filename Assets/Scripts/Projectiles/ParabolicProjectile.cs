using UnityEngine;

/// <summary>
/// Proyectil que se mueve hacia su destino siguiendo la trayectoria de un tiro parabólico
/// </summary>
public class ParabolicProjectile : Projectile
{

    /// <summary>
    /// Máxima altura que alcanza durante la trayectoria
    /// </summary>
    public float height;

    public void Update()
    {
        Vector3 position;
        position.x = Mathf.Lerp(positionStart.x, positionEnd.x, time);
        position.y = time * (2 * time * (positionStart.y - 2 * height + positionEnd.y) - 3 * positionStart.y + 4 * height - positionEnd.y) + positionStart.y;
        position.z = Mathf.Lerp(positionStart.z, positionEnd.z, time);
        transform.position = position;
        time += speed * Time.deltaTime;
        if (time >= 1)
            Impact();
    }

}
