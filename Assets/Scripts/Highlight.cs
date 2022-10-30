using System;
using UnityEngine;

/// <summary>
/// Resalte de una celda
/// </summary>
public class Highlight : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados del resalte
    /// <list type="bullet">
    /// <item><c>None</c>: celda sin resaltar</item>
    /// <item><c>Action</c>: celda sobre la que se encuentra el cursor</item>
    /// <item><c>Unit</c>: celda de la unidad seleccionada</item>
    /// <item><c>Target</c>: celda que es un objetivo posible</item>
    /// <item><c>AreaOfEffect</c>: celda que será afectada por la acción</item>
    /// <item><c>SelectedTarget</c>: celda del objetivo seleccionado</item>
    /// </list>
    /// </summary>
    [Flags]
    public enum State
    {
        None = 0,
        Cursor = 1,
        Unit = 2,
        Target = 4,
        AreaOfEffect = 8,
        SelectedTarget = 16
    }

    /// <summary>
    /// Resalte actual de la celda
    /// </summary>
    public State state;

    /// <summary>
    /// Componente que renderiza la malla
    /// </summary>
    private MeshRenderer meshRenderer;

    /// <summary>
    /// Material del centro
    /// </summary>
    private Material center;

    /// <summary>
    /// Material de los bordes
    /// </summary>
    private Material edge;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        center = materials[0];
        edge = materials[1];
    }

    /// <summary>
    /// Agrega el estado especificado al resalte de la celda
    /// </summary>
    /// <param name="state">El estado que se agrega</param>
    public void Add(State state)
    {
        this.state |= state;
        SetColor(GetColor());
    }

    /// <summary>
    /// Elimina el estado especificado al resalte de la celda
    /// </summary>
    /// <param name="state">El estado que se elimina</param>
    public void Remove(State state)
    {
        this.state &= ~state;
        SetColor(GetColor());
    }

    /// <summary>
    /// Regresa el color que debe tener el resalte de acuerdo al estado actual
    /// </summary>
    /// <returns>El color que debe tener el resalte</returns>
    private Color GetColor()
    {
        if (state.HasFlag(State.SelectedTarget))
            return Color.red;
        if (state.HasFlag(State.AreaOfEffect))
            return new Color(1, 1, 0);
        if (state.HasFlag(State.Target))
            return Color.blue;
        if (state.HasFlag(State.Unit))
            return Color.green;
        if (state.HasFlag(State.Cursor))
            return Color.cyan;
        return Color.clear;
    }

    /// <summary>
    /// Asigna el color especificado al resalte
    /// </summary>
    /// <param name="color">El color que se asigna al resalte</param>
    private void SetColor(Color color)
    {
        if (color == Color.clear)
            meshRenderer.enabled = false;
        else
        {
            color.a = .25f;
            center.color = color;
            color *= 16;
            edge.SetColor("_EmissionColor", color);
            meshRenderer.enabled = true;
        }
    }

}
