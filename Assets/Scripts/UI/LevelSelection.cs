using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Elemento de la interfaz de usuario que selecciona un nivel desde el men� principal
/// </summary>
public class LevelSelection : MonoBehaviour
{

    /// <summary>
    /// Imagen que muestra la vistra previa de un nivel
    /// </summary>
    public Image currentPreview;

    /// <summary>
    /// M�scara que de la vista previa que indica que el nivel est� bloqueado
    /// </summary>
    public GameObject locked;

    /// <summary>
    /// Control deslizante que permite cambiar de nivel r�pidamente
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Bot�n de retroceder un nivel
    /// </summary>
    public UnityEngine.UI.Button previous;

    /// <summary>
    /// Bot�n de avanzar un nivel
    /// </summary>
    public UnityEngine.UI.Button next;

    /// <summary>
    /// Bot�n de seleccionar el nivel actual
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
    /// Se manda a llamar cuando se presiona el bot�n de avanzar un nivel
    /// </summary>
    public void OnNext()
    {
        if (currentLevel < Level.lastLevel)
        {
            Audio.PlayClip(Audio.sounds.click2);
            slider.value = currentLevel + 1;
        }
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el bot�n de retroceder un nivel
    /// </summary>
    public void OnPrevious()
    {
        if (currentLevel > 1)
        {
            Audio.PlayClip(Audio.sounds.click2);
            slider.value = currentLevel - 1;
        }
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el bot�n de seleccionar el nivel actual
    /// </summary>
    public void OnSelect()
    {
        if (currentLevel <= PlayerPrefs.GetInt("unlocked", 1))
        {
            Audio.PlayClip(Audio.sounds.click2);
            Scene.GoToLevel(currentLevel);
        }
    }

    /// <summary>
    /// Actualiza la vista previa del nivel actual
    /// </summary>
    public void Refresh()
    {
        currentLevel = (uint)slider.value;
        previous.interactable = currentLevel > 1;
        next.interactable = currentLevel < Level.lastLevel;
        currentPreview.sprite = UI.sprites.previews[currentLevel - 1];
        if (currentLevel <= PlayerPrefs.GetInt("unlocked", 1))
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
