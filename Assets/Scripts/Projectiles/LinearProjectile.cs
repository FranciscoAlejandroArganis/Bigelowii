using UnityEngine;

/// <summary>
/// Proyectil que se mueve hacia su destino en línea recta
/// </summary>
public class LinearProjectile : Projectile
{

    public void Update()
    {
        transform.position = Vector3.Lerp(positionStart, positionEnd, time);
        time += speed * Time.deltaTime;
        if (time >= 1)
            Impact();
    }

}
