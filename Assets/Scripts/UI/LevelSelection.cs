using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Elemento de la interfaz de usuario que selecciona un nivel desde el menú principal
/// </summary>
public class LevelSelection : MonoBehaviour
{

    public static uint unlocked = 5;

    /// <summary>
    /// Arreglo con los sprites de las vistas previas de los niveles
    /// </summary>
    public Sprite[] previews;

    /// <summary>
    /// Imagen que muestra la vistra previa de un nivel
    /// </summary>
    public Image currentPreview;

    /// <summary>
    /// Máscara que de la vista previa que indica que el nivel está bloqueado
    /// </summary>
    public GameObject locked;

    /// <summary>
    /// Control deslizante que permite cambiar de nivel rápidamente
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Botón de retroceder un nivel
    /// </summary>
    public UnityEngine.UI.Button previous;

    /// <summary>
    /// Botón de avanzar un nivel
    /// </summary>
    public UnityEngine.UI.Button next;

    /// <summary>
    /// Botón de seleccionar el nivel actual
    /// </summary>
    public UnityEngine.UI.Button select;

    /// <summary>
    /// Identificador del nivel del cual se muestra actualmente la vista previa
    /// </summary>
    private uint currentLevel;

    public void Start()
    {
        Level.lastLevel = (uint)slider.maxValue;
        Refresh();
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de avanzar un nivel
    /// </summary>
    public void OnNext()
    {
        if (currentLevel < Level.lastLevel)
            slider.value = currentLevel + 1;
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de retroceder un nivel
    /// </summary>
    public void OnPrevious()
    {
        if (currentLevel > 1)
            slider.value = currentLevel - 1;
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el botón de seleccionar el nivel actual
    /// </summary>
    public void OnSelect()
    {
        if (currentLevel <= unlocked)
            Scene.GoToLevel(currentLevel);
    }

    /// <summary>
    /// Actualiza la vista previa del nivel actual
    /// </summary>
    public void Refresh()
    {
        currentLevel = (uint)slider.value;
        previous.interactable = currentLevel > 1;
        next.interactable = currentLevel < Level.lastLevel;
        currentPreview.sprite = previews[currentLevel - 1];
        if (currentLevel <= unlocked)
        {
            select.interactable = true;
            locked.SetActive(false);
        }
        else
        {
            select.interactable = false;
            locked.SetActive(true);
        }
    }

}
