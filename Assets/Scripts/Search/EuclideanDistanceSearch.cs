using UnityEngine;

/// <summary>
/// B�squeda de celdas usando la distancia euclidiana
/// <para>Encuentra todas las celdas que se encuentran hasta una cierta distancia del centro de la celda de origen</para>
/// </summary>
public class EuclideanDistanceSearch : Search
{

    /// <summary>
    /// Radio de b�squeda
    /// </summary>
    private float radius;

    /// <summary>
    /// Construye una nueva b�squeda por distancia euclidiana
    /// </summary>
    /// <param name="radius">El radio de b�squeda</param>
    public EuclideanDistanceSearch(float radius) : base()
    {
        this.radius = radius;
    }

    public override void FindCells(Cell startingCell)
    {
        foreach (Collider collider in Physics.OverlapSphere(startingCell.transform.position, radius, Utilities.mapLayer))
        {
            Cell currentCell = collider.GetComponent<Cell>();
            results.Add(currentCell);
        }
    }

}
