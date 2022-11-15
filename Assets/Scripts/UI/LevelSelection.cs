using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Elemento de la interfaz de usuario que selecciona un nivel desde el menú principal
/// </summary>
public class LevelSelection : MonoBehaviour
{

    /// <summary>
    /// Arreglo con los sprites de las vistas previas de los niveles
    /// </summary>
    public Sprite[] previews;

    /// <summary>
    /// Imagen que muestra la vistra previa de un nivel
    /// </summary>
    public Image currentPreview;

    /// <summary>
    /// Botón de retroceder un nivel
    /// </summary>
    public UnityEngine.UI.Button previous;

    /// <summary>
    /// Botón de avanzar un nivel
    /// </summary>
    public UnityEngine.UI.Button next;

    /// <summary>
    /// Identificador del último nivel
    /// </summary>
    public uint lastLevel;

    /// <summary>
    /// Identificador del nivel del cual se muestra actualmente la vista previa
    /// </summary>
    private uint currentLevel;

    public void Start()
    {
        Level.lastLevel = lastLevel;
        currentLevel = 1;
        Refresh();
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de avanzar un nivel
    /// </summary>
    public void OnNext()
    {
        if (currentLevel != lastLevel)
        {
            currentLevel++;
            Refresh();
        }
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de retroceder un nivel
    /// </summary>
    public void OnPrevious()
    {
        if (currentLevel != 1)
        {
            currentLevel--;
            Refresh();
        }
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de seleccionar el nivel actual
    /// </summary>
    public void OnSelect()
    {
        Scene.GoToLevel(currentLevel);
    }

    /// <summary>
    /// Actualiza la vista previa del nivel actual
    /// </summary>
    private void Refresh()
    {
        previous.interactable = currentLevel > 1;
        next.interactable = currentLevel < lastLevel;
        currentPreview.sprite = previews[currentLevel - 1];
    }

}
