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
