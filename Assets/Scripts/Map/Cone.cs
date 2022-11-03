using UnityEngine;

/// <summary>
/// Recurso que puede ser recolectado por esferas
/// </summary>
public class Cone : MonoBehaviour
{

    public void Start()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 2, Utilities.mapLayer))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            cell.cones.Add(this);
        }
    }

}
