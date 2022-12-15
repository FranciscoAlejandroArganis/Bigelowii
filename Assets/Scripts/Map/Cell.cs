using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Celda del mapa
/// </summary>
public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [Flags]
    public enum ActionFlags
    {
        None = 0,
        Recruit = 1,
        CallLightning = 2
    }

    /// <summary>
    /// Regresa la celda que se encuentra debajo de la posición especificada
    /// </summary>
    /// <param name="position">La posición debajo de la cual se busca una celda</param>
    /// <returns>La celda debajo de <c>position</c> o <c>null</c> si no hay ninguna celda</returns>
    public static Cell Below(Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1, Utilities.mapLayer))
            return hit.collider.GetComponent<Cell>();
        return null;
    }

    /// <summary>
    /// Resalte de la celda
    /// </summary>
    public Highlight highlight;

    /// <summary>
    /// Arreglo con los vecinos de la celda
    /// </summary>
    public Cell[] neighbors;

    /// <summary>
    /// Unidad que se encuentra sobre la celda
    /// </summary>
    public Unit unit;

    public ActionFlags actionFlags;

    /// <summary>
    /// <c>true</c> si la celda ha pasado por la cola del recorrido BFS
    /// </summary>
    public bool visited;

    /// <summary>
    /// La celda desde donde se alacanzó esta celda en el recorrido BFS
    /// </summary>
    public Cell predecesor;

    /// <summary>
    /// Longitud del camino desde la celda donde inció el recorrido BFS hasta esta celda
    /// </summary>
    public uint distance;

    /// <summary>
    /// Lista con los conos que se encuentran sobre la celda
    /// </summary>
    public List<Cone> cones;

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

    /// <summary>
    /// Regresa <c>true</c> si la celda está libre
    /// </summary>
    /// <returns><c>true</c> si la celda no hay conos ni unidades sobre la celda</returns>
    public bool IsFree()
    {
        return cones.Count == 0 && !unit;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Level.state == Level.State.Human)
        {
            highlight.Add(Highlight.State.Cursor);
            switch (Turn.state)
            {
                case Turn.State.Unit:
                    if (unit)
                    {
                        highlight.Add(Highlight.State.Unit);
                        UI.primaryUnit.unit = unit;
                        UI.primaryUnit.Show();
                    }
                    break;
                case Turn.State.Action:
                    break;
                case Turn.State.Target:
                    if (highlight.state.HasFlag(Highlight.State.Target))
                    {
                        Turn.action.AddTargetHighlight(this);
                        if (unit && unit != Turn.selectedUnit)
                        {
                            UI.secondaryUnit.unit = unit;
                            UI.secondaryUnit.Show();
                        }
                    }
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.Remove(Highlight.State.Cursor);
        if (Level.state == Level.State.Human)
        {
            switch (Turn.state)
            {
                case Turn.State.Unit:
                    if (unit)
                    {
                        highlight.Remove(Highlight.State.Unit);
                        UI.primaryUnit.Hide();
                    }
                    break;
                case Turn.State.Action:
                    break;
                case Turn.State.Target:
                    if (highlight.state.HasFlag(Highlight.State.Target))
                    {
                        Turn.action.RemoveTargetHighlight(this);
                        if (unit && unit != Turn.selectedUnit)
                            UI.secondaryUnit.Hide();
                    }
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Level.state == Level.State.Human)
        {
            switch (Turn.state)
            {
                case Turn.State.Unit:
                    if (unit)
                    {
                        Turn.SelectUnit(unit);
                        Audio.PlayClip(Audio.sounds.click);
                    }
                    break;
                case Turn.State.Action:
                    if (unit)
                    {
                        if (unit != Turn.selectedUnit)
                        {
                            Turn.DeselectUnit();
                            Turn.SelectUnit(unit);
                            Audio.PlayClip(Audio.sounds.click);
                        }
                    }
                    else
                        Turn.DeselectUnit();
                    break;
                case Turn.State.Target:
                    if (highlight.state.HasFlag(Highlight.State.Target))
                    {
                        Turn.SelectTarget(this);
                        Audio.PlayClip(Audio.sounds.click);
                    }
                    else
                        Turn.CancelAction();
                    break;
            }
        }
    }

}
