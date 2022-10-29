using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Celda del mapa
/// </summary>
public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    /// <summary>
    /// Resalte de la celda
    /// </summary>
    public Highlight highlight;

    /// <summary>
    /// <c>true</c> si la celda es terreno y no pueden haber unidades encima
    /// </summary>
    public bool terrain;

    /// <summary>
    /// Arreglo con los vecinos de la celda
    /// </summary>
    public Cell[] neighbors;

    /// <summary>
    /// Unidad que se encuentra sobre la celda
    /// </summary>
    public Unit unit;

    public void Start()
    {
        highlight = GetComponentInChildren<Highlight>();
        neighbors = new Cell[6];
        int index = 0;
        while (index < 6)
        {
            bool intersection = Physics.Raycast(transform.position, Utilities.roots[index], out RaycastHit hit, 1, Utilities.mapLayer);
            neighbors[index] = intersection ? hit.collider.GetComponent<Cell>() : null;
            index++;
        }
    }

    /// <summary>
    /// Determina si la celda especificada es adyacente a esta celda y regresa su índice de adyacencia
    /// </summary>
    /// <param name="cell">La celda de la que se determina el índice de adyacencia</param>
    /// <returns>El índice de adyacencia de <c>cell</c> o 6 si no es un vecino de esta celda</returns>
    public uint EdgeIndex(Cell cell)
    {
        uint index = 0;
        while (index < 6)
        {
            if (cell == neighbors[index]) break;
            index++;
        }
        return index;
    }

    /// <summary>
    /// Regresa la posición que tendría la unidad si estuviera encima de la celda
    /// </summary>
    /// <param name="unit">La unidad de la que se determina su posición</param>
    /// <returns>La posición que debe tener la unidad al estar sobre la celda</returns>
    public Vector3 UnitPosition(Unit unit)
    {
        return new Vector3(transform.position.x, unit.transform.position.y, transform.position.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.Add(Highlight.State.Cursor);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.Remove(Highlight.State.Cursor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //
    }


}
