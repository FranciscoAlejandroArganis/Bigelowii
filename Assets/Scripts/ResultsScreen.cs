using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Componente encargado de la pantalla con los resultados del nivel
/// </summary>
public class ResultsScreen : MonoBehaviour
{

    /// <summary>
    /// Es <c>true</c> si el nivel ha terminado en victoria para el jugador
    /// </summary>
    public static bool victory;

    /// <summary>
    /// El identificador del siguiente nivel
    /// </summary>
    public static uint nextLevel;

    /// <summary>
    /// Diccionario con la información de las unidades que el jugador eliminó en el nivel
    /// <para>Mapea el sprite de un tipo de unidad a la cantidad de unidades eliminadas de ese tipo</para>
    /// </summary>
    public static Dictionary<Sprite, uint> unitsKilled;

    /// <summary>
    /// Diccionario con la información de las unidades que el jugador perdió en el nivel
    /// <para>Mapea el sprite de un tipo de unidad la cantidad de unidades perdidas de ese tipo</para>
    /// </summary>
    public static Dictionary<Sprite, uint> unitsLost;

    /// <summary>
    /// Establece el resultado del nivel
    /// </summary>
    /// <param name="victory">Es <c>true</c> si el nivel terminó en victoria para el jugador</param>
    /// <param name="nextLevel">El identificador del siguiente nivel</param>
    /// <param name="player">El jugador del cual se muestran las unidades eliminadas y perdidas</param>
    public static void SetLevelResult(bool victory, uint nextLevel, Player player)
    {
        ResultsScreen.victory = victory;
        ResultsScreen.nextLevel = nextLevel;
        if (nextLevel <= Level.lastLevel && nextLevel > PlayerPrefs.GetInt("unlocked", 1))
            PlayerPrefs.SetInt("unlocked", (int)nextLevel);
        unitsKilled = player.unitsKilled;
        unitsLost = player.unitsLost;
    }

    /// <summary>
    /// El prefab del ícono de una unidad
    /// </summary>
    public Image unitIcon;

    /// <summary>
    /// Objeto que contiene los íconos de las unidades eliminadas
    /// </summary>
    public GameObject unitsKilledContent;

    /// <summary>
    /// Objeto que contiene los íconos de las unidades perdidas
    /// </summary>
    public GameObject unitsLostContent;

    /// <summary>
    /// Botón de continuar
    /// </summary>
    public UnityEngine.UI.Button continueButton;

    public void Start()
    {
        DisplayDictionaryUnits(unitsKilled, unitsKilledContent);
        DisplayDictionaryUnits(unitsLost, unitsLostContent);
        unitsKilled = null;
        unitsLost = null;
        if (nextLevel > Level.lastLevel)
            continueButton.gameObject.SetActive(false);
        else
            continueButton.image.sprite = victory ? UI.sprites.next : UI.sprites.retry;
    }

    /// <summary>
    /// Hace que el juego continue al siguiente nivel
    /// </summary>
    public void Continue()
    {
        Scene.GoToLevel(nextLevel);
    }

    /// <summary>
    /// Hace que se muestren las unidades según la información del diccionario especificado
    /// </summary>
    /// <param name="dictionary">El diccionario con la información de las unidades</param>
    /// <param name="content">El objeto donde se mostrarán los íconos de las unidades</param>
    private void DisplayDictionaryUnits(Dictionary<Sprite, uint> dictionary, GameObject content)
    {
        foreach (KeyValuePair<Sprite, uint> pair in dictionary)
        {
            uint index = 1;
            while (index <= pair.Value)
            {
                Image image = Instantiate(unitIcon, content.transform);
                image.sprite = pair.Key;
                index++;
            }
        }
    }

}
