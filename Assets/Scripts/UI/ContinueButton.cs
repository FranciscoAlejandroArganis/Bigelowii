using UnityEngine.SceneManagement;

/// <summary>
/// Botón de continuar en la pantalla de resultados
/// </summary>
public class ContinueButton : Button
{

    public override void OnClick()
    {
        Level.currentTime = 0;
        Timeline.events.Clear();
        SceneManager.LoadScene("Level " + ResultsScreen.nextLevel);
    }

}
