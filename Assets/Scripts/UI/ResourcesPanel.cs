using System;
using UnityEngine;

/// <summary>
/// Panel que muestra los conos que tiene actualmente el jugador
/// </summary>
public class ResourcesPanel : Panel
{

    /// <summary>
    /// Objeto que contiene los íconos de los conos
    /// </summary>
    public GameObject content;

    /// <summary>
    /// Cantidad de conos que se muestran en el panel
    /// </summary>
    private int cones;

    /// <summary>
    /// Actualiza la cantidad de conos que se muestran en el panel para que coincidan con los conos que tiene actualmente el jugador
    /// </summary>
    public void UpdateCones()
    {
        int difference = cones - Level.cones;
        if (difference < 0)
        {
            // Se muestran menos conos que los que tiene el jugador
            while (true)
            {
                UI.NewConeIcon();
                difference++;
                if (difference == 0)
                    break;
            }
        }
        else if (difference > 0)
        {
            // Se muestran más conos que los que tiene el jugador
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
                difference--;
                if (difference == 0)
                    break;
            }
        }
        cones = Level.cones;
    }

    public override void Show()
    {
        throw new NotImplementedException();
    }

    public override void Hide()
    {
        throw new NotImplementedException();
    }

}
