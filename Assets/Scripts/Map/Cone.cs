using UnityEngine;

/// <summary>
/// Recurso que puede ser recolectado por esferas
/// </summary>
public class Cone : MonoBehaviour
{

    public void Start()
    {
        Cell cell = Cell.Below(transform.position);
        if (cell)
            cell.cones.Add(this);
    }

}
