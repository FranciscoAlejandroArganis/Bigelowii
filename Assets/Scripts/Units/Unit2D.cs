using UnityEngine;

/// <summary>
/// Unidad 2D
/// </summary>
public abstract class Unit2D : Unit
{

    public void Update()
    {
        transform.rotation = CameraController.instance.transform.rotation;
    }

    public override bool Rotates()
    {
        return false;
    }

}
