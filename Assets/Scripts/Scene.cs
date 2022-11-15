using UnityEngine.SceneManagement;

/// <summary>
/// Componente encargado de manejar las transiciones de escenas
/// </summary>
public class Scene
{
    
    /// <summary>
    /// Transiciona a la escena del menú principal
    /// </summary>
    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Transiciona a la escena de la pantalla de resultados
    /// </summary>
    public static void GoToResultsScreen()
    {
        SceneManager.LoadScene("Results Screen");
    }

    /// <summary>
    /// Transiciona a la escena del nivel especificado
    /// </summary>
    /// <param name="id">El identificador del nivel</param>
    public static void GoToLevel(uint id)
    {
        Level.currentTime = 0;
        Timeline.events.Clear();
        SceneManager.LoadScene("Level " + id);
    }

}
