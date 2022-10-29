using UnityEngine;

/// <summary>
/// Unidad 2D
/// </summary>
public abstract class Unit2D : Unit
{

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(CameraController.position - transform.position);
    }

}
