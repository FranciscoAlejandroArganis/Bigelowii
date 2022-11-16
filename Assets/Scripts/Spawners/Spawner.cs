using UnityEngine;

/// <summary>
/// Generador de nuevas unidades
/// </summary>
public abstract class Spawner : MonoBehaviour
{

    /// <summary>
    /// Celda a partir de la que se busca un punto de aparición
    /// </summary>
    public Cell cell;

    /// <summary>
    /// Plantilla que se usa para generar nuevas unidades
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Jugador al que pertencen las unidades generadas
    /// </summary>
    public Player player;

    /// <summary>
    /// Cantidad de unidades de tiempo que tienen que pasar antes de que se vuelvan a generar unidades
    /// </summary>
    public uint cooldown;

    /// <summary>
    /// Tiempo en el que que se generaron unidades la última vez
    /// </summary>
    public int time;

    /// <summary>
    /// Búsqueda de un punto de aparición
    /// </summary>
    protected FirstMatchSearch search;

    public void Start()
    {
        search = new FirstMatchSearch(SpawnPoint);
        cell = Cell.Below(transform.position);
    }

    /// <summary>
    /// Genera nuevas unidades de acuerdo al estado actual del nivel
    /// </summary>
    /// <returns><c>true</c> si se generó al menos una nueva unidad</returns>
    public abstract bool Spawn();

    /// <summary>
    /// Regresa un punto de aparción
    /// </summary>
    /// <returns>Una celda donde puede aparecer una nueva unidad o <c>null</c> si no hay celdas libres</returns>
    protected Cell GetSpawnLocation()
    {
        search.FindCells(cell);
        Cell freeCell = search.firstMatch;
        foreach (Cell cell in search.results)
        {
            cell.visited = false;
            cell.distance = 0;
            cell.predecesor = null;
        }
        search.firstMatch = null;
        search.results.Clear();
        return freeCell;
    }

    /// <summary>
    /// Predicado que determina si en una celda puede aparecer una nueva unidad
    /// </summary>
    /// <param name="cell">La celda que se prueba</param>
    /// <returns><c>true</c> si la celda está libre</returns>
    protected bool SpawnPoint(Cell cell)
    {
        return cell.IsFree();
    }

}
